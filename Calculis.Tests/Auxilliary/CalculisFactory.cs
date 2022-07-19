using Calculis.Core.Calculation;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Test.Auxilliary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Tests.Auxilliary
{
    internal class CalculisFactory
    {
        //private static IDictionary<string, IItem> _items { get; set; }

        internal static Engine Create(double[] values, DateTime? initialDT = null)
        {
            var _items = CreateItems(values).ToDictionary(x => x.Name);
            TestTimeProvider? timeProvider = null;

            if (initialDT != null)
                timeProvider = new TestTimeProvider((DateTime)initialDT);

            return new Engine(_items.Values, timeProvider ?? null);
        }

        internal static Engine Create(ICollection<string> names, DateTime? initialDT = null)
        {
            TestTimeProvider timeProvider = null;

            if (initialDT != null)
                timeProvider = new TestTimeProvider((DateTime)initialDT);

            var dataItems = new List<DataItem>();
            foreach (var name in names)
                dataItems.Add(new DataItem(name));

            return new Engine(dataItems, timeProvider ?? null);
        }

        private static IEnumerable<IItem> CreateItems(double[] values)
        {
            var count = 0;
            var items = new List<IItem>();

            foreach (var value in values)
                items.Add(new DataItem($"i{++count}") { Value = value });

            return items;
        }
    }
}
