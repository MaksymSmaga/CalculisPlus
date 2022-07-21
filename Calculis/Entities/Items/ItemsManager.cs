using Calculis.Core.Convert;
using Calculis.Core.Entities.Functions;
using Calculis.Core.Entities.Functions.Abstractions.Base;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Calculis.Core.Entities.Items
{
    internal sealed class ItemsManager
    {
        internal IDictionary<string, string> ExpressionAlias { get; private set; }
                                             = new Dictionary<string, string>();
        internal event EventHandler<UpdateArgs> Updating;

        private readonly IDictionary<string, IItem> _items;
        private readonly IDictionary<string, string> _itemsNames;
        private readonly IDictionary<string, ItemInfo> _aliasFunctions = new Dictionary<string, ItemInfo>();

        private int _count;
        private int _fncCount;

        private CultureInfo _culture;
        private ExpressionParser _expressionParser;

        internal ItemsManager(IEnumerable<IItem> items)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
        }

        internal CalcItem Create(string name, string expression, CultureInfo culture)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (_itemsNames.ContainsKey(name)) throw new ArgumentException($"The name {name} has already used!");

            _culture = culture;
            _expressionParser = new ExpressionParser(this, culture);

            _itemsNames.Add(name, $"item{++_count}");

            foreach (var itemName in _itemsNames.OrderByDescending(x => x.Key.Length))
                expression = expression.Replace(itemName.Key, itemName.Value);

            var expressions = _expressionParser.Parse(expression);

            FunctionDescription functionDescription;
            IItem item = null;
            foreach (var functionExpression in expressions)
            {
                functionDescription = new FunctionDescription(functionExpression);
                item = _aliasFunctions[ExpressionAlias[functionExpression]].Item = CreateItem(FunctionManager.Create(functionDescription.Name, ExtractArgs(functionExpression)));
            }

            _items.Add(_itemsNames[name], item);

            return (CalcItem)item;
        }

        /// <summary>
        /// Creates CalcItem,
        /// Calcs its Value from function-argument,
        /// Updates the item data.
        /// </summary>
        /// <param name="function">function-argument</param>
        /// <returns>new CalcItem</returns>
        private CalcItem CreateItem(BaseFunction function)
        {
            var item = new CalcItem(function);
            Updating += item.Update;

            return item;
        }

        /// <summary>
        /// Return Item by its name.
        /// </summary>
        /// <param name="name">Item name argument</param>
        /// <returns>Item</returns>
        /// <exception cref="NullReferenceException">Item {name} does not exist!</exception>
        internal IItem GetItem(string name)
        {
            return _items.TryGetValue(_itemsNames[name], out var item) ?
                          item : throw new NullReferenceException($"Item {name} does not exist!");
        }







        internal void Update(DateTime timestamp)
        {
            Updating?.Invoke(this, new UpdateArgs { TimeStamp = timestamp });
        }














        private IList<IItem> ExtractArgs(string expression)
        {
            var functionDescription = new FunctionDescription(expression);
            var args = new List<IItem>();

            foreach (var argString in functionDescription.Args)
            {
                var arg = GetArg(argString, _items.ContainsKey, (key) => _items[key]) ??
                          GetArg(argString, _aliasFunctions.ContainsKey, (key) => _aliasFunctions[key].Item) ??
                          GetArg(argString, IsDouble, (expr) => new ConstantItem(double.Parse(expr, NumberStyles.Float, _culture.NumberFormat)));

                args.Add(arg ?? throw new ArgumentOutOfRangeException(argString));
            }

            return args;
        }




        /// <summary>
        /// Checks - does expression contains double value.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>True if expression contains double</returns>
        private bool IsDouble(string expression) =>
                     double.TryParse(expression, NumberStyles.Float, _culture.NumberFormat, out double result);




        private IItem GetArg(string argString, Func<string, bool> checkingFunction, Func<string, IItem> creationFunction)
        {
            IItem newItem = null;

            var isDenominator = false;
            var isNegative = false;

            if (argString.Substring(0, 1) == "/")
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
                if (isNegative) newItem = new CalcItem(FunctionManager.Create("MUL", new List<IItem> { new ConstantItem(-1), newItem }));
                if (isDenominator) newItem = new CalcItem(FunctionManager.Create("DIV", new List<IItem> { new ConstantItem(1), newItem }));
            }

            return newItem;
        }





        /// <summary>
        /// Gets Expression by its alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns>string expression</returns>
        internal string GetExpression(string alias) => _aliasFunctions[alias].ReplacedExpression;

        /// <summary>
        /// Adds new alias to ExpressionAlias and _aliasFunctions.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="original"></param>
        /// <returns>new alias</returns>
        internal string AddAlias(string expression, string original)
        {
            // To create alias increasing function count + 2.
            string alias = $"__fnc{++_fncCount}";

            // To add new alias to IDictionary<string, string>.
            ExpressionAlias.Add(expression, alias);

            // To add new alias to IDictionary<string, ItemInfo>.
            _aliasFunctions.Add(alias, new ItemInfo
            {
                Alias = alias,
                OriginalExpression = original,
                ReplacedExpression = expression
            });

            return alias;
        }






        internal ICollection<string> GetHint(string expression, int position)
        {
            var hintCollection = new List<string>();

            var typingStart = -1;
            var typingEnd = expression.Length;

            for (int i = position; i >= 0; i--)
                if ("(;+-/* ".Contains(expression[i]))
                {
                    typingStart = i;
                    break;
                }

            for (int i = position; i < expression.Length; i++)
                if (");+-/* ".Contains(expression[i]))
                {
                    typingEnd = i;
                    break;
                }


            var typingElement = expression.Substring(typingStart + 1, typingEnd - typingStart - 1);

            //elements
            foreach (var item in _itemsNames.Keys)
                if (item.Contains(typingElement))
                    hintCollection.Add(item);

            //functions
            foreach (var funcName in FunctionManager.Functions.Keys)
                if (funcName.Contains(typingElement))
                    hintCollection.Add(funcName);

            return hintCollection;
        }
    }
}
