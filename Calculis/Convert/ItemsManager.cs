using Calculis.Core.Auxilliary;
using Calculis.Core.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Calculis.Core.Convert
{
    internal sealed class ItemsManager
    {
        private IDictionary<string, IValueItem> _items;
        private IDictionary<string, string> _itemsNames;

        private IDictionary<string, ItemInfo> _aliasFunctions = new Dictionary<string, ItemInfo>();
        internal IDictionary<string, string> ExpressionAlias { get; private set; } = new Dictionary<string, string>();

        private int _count;
        private int _fncCount;

        internal event EventHandler<UpdateArgs> Updating;


        private ExpressionParser _expressionParser;

        internal ItemsManager(IEnumerable<IValueItem> items)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
        }

        internal CalculatingItem Create(string name, string expression)
        {
            if(name == null) throw new ArgumentNullException("name");
            if (_itemsNames.ContainsKey(name)) throw new ArgumentException($"The name {name} has already used!");

            _itemsNames.Add(name, $"item{++_count}");

            foreach (var itemName in _itemsNames.OrderByDescending(x => x.Key.Length))
                expression = expression.Replace(itemName.Key, itemName.Value);

            _expressionParser = new ExpressionParser(this);
            var expressions = _expressionParser.Parse(expression);

            FunctionDescription functionDescription;
            IValueItem item = null;
            foreach (var functionExpression in expressions)
            {
                functionDescription = new FunctionDescription(functionExpression);
                item = _aliasFunctions[ExpressionAlias[functionExpression]].Item = CreateItem(FunctionManager.Create(functionDescription.Name, ExtractArgs(functionExpression)));
            }
                

            return (CalculatingItem)item;
        }

        internal void Update(DateTime timestamp)
        {
            Updating?.Invoke(this, new UpdateArgs { Timestamp = timestamp });
        }

        private CalculatingItem CreateItem(FunctionBase function)
        {
            var item = new CalculatingItem(function);
            Updating += item.Update;

            return item;
        }

        private IList<IValueItem> ExtractArgs(string expression)
        {
            var functionDescription = new FunctionDescription(expression);
            var args = new List<IValueItem>();
            foreach (var argString in functionDescription.Args)
            {
                var arg = GetArg(argString, _items.ContainsKey, (key) => _items[key]) ??
                          GetArg(argString, _aliasFunctions.ContainsKey, (key) => _aliasFunctions[key].Item) ??
                          GetArg(argString, isDouble, (expr) => new ConstantItem(double.Parse(expr)));

                args.Add(arg ?? throw new ArgumentOutOfRangeException(argString));
            }

            return args;
        }

        private bool isDouble(string expression)
        {
            return double.TryParse(expression.Replace('.', ','), out double result);
        }

        private IValueItem GetArg(string argString, Func<string, bool> checkingFunction, Func<string, IValueItem> creationFunction)
        {
            IValueItem newItem = null;

            var isDenominator = false;
            var isNegative = false;

            if(argString.Substring(0, 1) == "/")
            {
                argString = argString.Replace("/", "");
                isDenominator = true;
            }
            if (argString.Substring(0, 1) == "-")
            {
                argString = argString.Replace("-", "");
                isNegative = true;
            }
            if (checkingFunction(argString))
            {
                newItem = creationFunction(argString);
                if (isNegative) newItem = new CalculatingItem(FunctionManager.Create("MUL", new List<IValueItem> { new ConstantItem(-1), newItem }));
                if (isDenominator) newItem = new CalculatingItem(FunctionManager.Create("DIV", new List<IValueItem> { new ConstantItem(1), newItem }));
            }

            return newItem;
        }

        public string GetExpression(string alias)
        {
            return _aliasFunctions[alias].ReplacedExpression;
        }

        internal string AddAlias(string expression, string original)
        {
            string alias = $"__fnc{++_fncCount}";
            ExpressionAlias.Add(expression, alias);
            _aliasFunctions.Add(alias, new ItemInfo { Alias = alias, OriginalExpression = original, ReplacedExpression = expression });

            return alias;
        }
    }

    struct FunctionDescription
    {
        internal string Name;
        internal string[] Args;

        internal FunctionDescription(string expression)
        {
            var bracketIndex = expression.IndexOf('(');
            Name = expression.Substring(0, bracketIndex);
            Args = expression.Substring(bracketIndex).Trim('(', ')').Split(';');
        }
    }
}
