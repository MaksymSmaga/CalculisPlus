using Calculis.Core;
using Calculis.Core.Calculation;
using Calculis.Test.Auxilliary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Tests.Auxilliary
{
    internal class CalculisFactory
    {
        //private static IDictionary<string, IValueItem> _items { get; set; }

        internal static CalculisEngine Create(double[] values, DateTime? initialDT = null)
        {
            var _items = CreateItems(values).ToDictionary(x => x.Name);
            TestTimeProvider timeProvider = null;

            if (initialDT != null)
                timeProvider = new TestTimeProvider((DateTime)initialDT);

            return new CalculisEngine(_items.Values, timeProvider ?? null);
        }

        internal static CalculisEngine Create(ICollection<string> names, DateTime? initialDT = null)
        {
            TestTimeProvider timeProvider = null;

            if (initialDT != null)
                timeProvider = new TestTimeProvider((DateTime)initialDT);

            var dataItems = new List<DataItem>();
            foreach (var name in names)
                dataItems.Add(new DataItem(name));

            return new CalculisEngine(dataItems, timeProvider ?? null);
        }

        private static IEnumerable<IValueItem> CreateItems(double[] values)
        {
            var count = 0;
            var items = new List<IValueItem>();

            foreach (var value in values)
                items.Add(new DataItem($"i{++count}") { Value = value });

            return items;
        }
    }
}
