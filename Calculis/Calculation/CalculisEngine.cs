using System;
using Calculis.Core.Convert;
using System.Collections.Generic;
using System.Linq;
using Calculis.Core.Auxilliary;

namespace Calculis.Core.Calculation
{
    public sealed class CalculisEngine
    {
        private ExpressionConverter _converter;
        private readonly TimeProvider _timeProvider;

        private IEnumerable<string> _nameSchemes;
        private IDictionary<string, IValueItem> _items;
        private IDictionary<string, string> _itemsNames;

        private event EventHandler<UpdateArgs> _update;
        private int _count;

        public CalculisEngine(IEnumerable<IValueItem> items)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);

            _converter = new ExpressionConverter(_items, null);
        }

        public CalculisEngine(IEnumerable<IValueItem> items, IEnumerable<string> nameSchemes = null)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
            _nameSchemes = nameSchemes;

            _converter = new ExpressionConverter(_items, _nameSchemes);
        }

        public CalculisEngine(IEnumerable<IValueItem> items, TimeProvider timeProvider = null)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
            _timeProvider = timeProvider;

            _converter = new ExpressionConverter(_items, _nameSchemes);
        }

        public CalculisEngine(IEnumerable<IValueItem> items, IEnumerable<string> nameSchemes, TimeProvider timeProvider = null)
        {
            _itemsNames = items.ToDictionary(x => x.Name, x => $"item{++_count}");
            _items = items.ToDictionary(x => _itemsNames[x.Name]);
            _nameSchemes = nameSchemes;  
            _timeProvider = timeProvider;

            _converter = new ExpressionConverter(_items, _nameSchemes);
        }

        public CalculatingItem Add(string name, string expression)
        {
            if (_itemsNames.ContainsKey(name)) throw new ArgumentException($"The name {name} has already used!");

            _itemsNames.Add(name, $"item{++_count}");

            foreach (var itemName in _itemsNames)
                expression = expression.Replace(itemName.Key, itemName.Value);

            var newItem = _converter.ParseExpression(expression);

            _update += newItem.Update;
            newItem.Name = name;

            _items.Add(_itemsNames[name], newItem);

            return newItem;
        }

        public void Remove(string name)
        {
            _items.Remove(name);
        }

        public void Update()
        {
            _timeProvider?.Update();
            _update?.Invoke(this, new UpdateArgs { Timestamp = _timeProvider.Now });
        }
    }
}
