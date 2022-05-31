using Calculis.Core.Auxilliary;
using Calculis.Core.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Calculis.Core.Convert
{
    public sealed class ItemsManager
    {
        private IDictionary<string, IValueItem> _items;
        private IDictionary<string, string> _itemsNames;

        private IDictionary<string, ItemInfo> _aliasFunctions = new Dictionary<string, ItemInfo>();
        public IDictionary<string, string> ExpressionAlias { get; private set; } = new Dictionary<string, string>();

        private int _count;
        private int _fncCount;

        internal event EventHandler<UpdateArgs> Updating;


        private ExpressionParser _expressionParser;

        public ItemsManager(IEnumerable<IValueItem> items)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
        }

        internal CalculatingItem Create(string name, string expression)
        {
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

        public void Update(DateTime timestamp)
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
                if (_items.ContainsKey(argString))
                    args.Add(_items[argString]);
                else if (_items.ContainsKey(argString.Replace("-", "")))
                    args.Add(new CalculatingItem(FunctionManager.Create("MUL", new List<IValueItem> { new ConstantItem(-1), _items[argString.Replace("-", "")] })));
                else if (_aliasFunctions.ContainsKey(argString))
                    args.Add(_aliasFunctions[argString].Item);
                else if (_aliasFunctions.ContainsKey(argString.Replace("-", "")))
                    args.Add(new CalculatingItem(FunctionManager.Create("MUL", new List<IValueItem> { new ConstantItem(-1), _aliasFunctions[argString.Replace("-", "")].Item })));
                else if (double.TryParse(argString.Replace('.', ','), out double result))
                    args.Add(new ConstantItem(result));
                else
                    throw new ArgumentOutOfRangeException(argString);
            }

            return args;
        }

        public string GetExpression(string alias)
        {
            return _aliasFunctions[alias].ReplacedExpression;
        }

        public string AddAlias(string expression, string original)
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
