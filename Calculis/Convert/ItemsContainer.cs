using Calculis.Core.Auxilliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculis.Core.Convert
{
    /*public sealed class ItemsContainer
    {
        private IDictionary<string, IValueItem> _items;
        private IDictionary<string, string> _itemsNames;

        private IDictionary<string, ItemInfo> _aliasFunctions = new Dictionary<string, ItemInfo>();
        public IDictionary<string, string> ExpressionAlias { get; private set; } = new Dictionary<string, string>();

        private int _count;
        private ItemsManager _itemManager;

        internal event EventHandler<UpdateArgs> Updating;

        public ItemsContainer(IEnumerable<IValueItem> items, ItemsManager itemManager)
        {
            _itemManager = itemManager;
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
        }

        public CalculatingItem Add(string name, string expression)
        {
            if (_itemsNames.ContainsKey(name)) throw new ArgumentException($"The name {name} has already used!");

            _itemsNames.Add(name, $"item{++_count}");

            foreach (var itemName in _itemsNames.OrderByDescending(x => x.Key.Length))
                expression = expression.Replace(itemName.Key, itemName.Value);


            var newItem = _itemManager.Create(expression, this);
            newItem.Name = name;

            _items.Add(_itemsNames[name], newItem);

            return newItem;
        }

        public bool Contains(string alias)
        {
            return _aliasFunctions.ContainsKey(alias);
        }

        public IValueItem GetItem(string alias)
        {
            return _aliasFunctions[alias].Item;
        }

        public string GetExpression(string alias)
        {
            return _aliasFunctions[alias].ReplacedExpression;
        }

        public void AddAlias(string expression, string original, string alias)
        {
            ExpressionAlias.Add(expression, alias);
            _aliasFunctions.Add(alias, new ItemInfo { Alias = alias, OriginalExpression = original, ReplacedExpression = expression });
        }

        public void Update(DateTime timestamp)
        {
            Updating?.Invoke(this, new UpdateArgs { Timestamp = timestamp });
        }
    }*/

    class ItemInfo
    {
        public IValueItem Item { get; set; }
        public string Alias { get; set; }
        public string OriginalExpression { get; set; }
        public string ReplacedExpression { get; set; }
    }
}
