using Calculis.Core;
using Calculis.Core.Calculation;
using Calculis.Test.Auxilliary;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Calculis.Test
{
    public class FunctionsTest
    {
        private IDictionary<string, IValueItem> _items { get; set; }

        [Theory]
        [InlineData(1.0, 4.0)]
        public void Test_Sum_Success(double x, double y)
        {
            var engine = Create();

            _items["item1"].Value = x;
            _items["item2"].Value = y;

            var item = engine.Add("item3", "SUM(item1;item2)");

            Assert.Equal(x + y, item.Value);
        }

        [Fact]
        public void Test_LastOf_Success()
        {
            var engine = Create();



        }

        private IEnumerable<IValueItem> CreateItems()
        {
            return new List<IValueItem>() { new DataItem("item1"), new DataItem("item2") };
        }

        private CalculisEngine Create()
        {
            _items = CreateItems().ToDictionary(x => x.Name);
            var names = new List<string>() { "item" };

            return new CalculisEngine(_items.Values, names);
        }
    }
}
