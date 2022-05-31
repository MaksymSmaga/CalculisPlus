using System;
using Calculis.Core.Convert;
using System.Collections.Generic;
using System.Linq;
using Calculis.Core.Auxilliary;

namespace Calculis.Core.Calculation
{
    public sealed class CalculisEngine
    {
        //private IEnumerable<string> _nameSchemes;
        private readonly ItemsManager _itemsManager;
        private readonly TimeProvider _timeProvider;

        public CalculisEngine(IEnumerable<IValueItem> items, TimeProvider timeProvider = null)
        {
            _timeProvider = timeProvider ?? new DefaultTimeProvider();
            _itemsManager = new ItemsManager(items);
        }

        public CalculatingItem Add(string name, string expression)
        {
            return _itemsManager.Create(name, expression); ;
        }

        public void Remove(string name)
        {
            //_items.Remove(name);
        }

        public void Update()
        {
            _timeProvider?.Update();
            _itemsManager.Update(_timeProvider.Now);
        }
    }
}
