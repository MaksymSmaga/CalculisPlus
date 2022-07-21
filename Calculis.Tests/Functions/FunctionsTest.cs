using Calculis.Core.Calculation;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Test.Auxilliary;
using Calculis.Tests.Auxilliary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Calculis.Tests.Functions
{
    public class FunctionsTest
    {
        private IDictionary<string, IItem> _items { get; set; }

        [Theory]
        [InlineData(1.0, 4.0)]
        public void Test_Sum_Success(double x, double y)
        {
            var engine = Create();

            _items["Jimmy"].Value = x;
            _items["Billy"].Value = y;

            var item = engine.Add("foo", "SUM(Jimmy;Billy)");

            Assert.Equal(x + y, item.Value);
        }

        [Theory]
        [InlineData(1.0, 4.0)]
        public void Test_Arithmetic_Success(double x, double y)
        {
            var engine = Create();

            _items["Jimmy"].Value = x;
            _items["Billy"].Value = y;

            var item = engine.Add("foo", $"Jimmy+{0.3.ToString(CultureInfo.CurrentCulture.NumberFormat)}");


            Assert.Equal(1.3, item.Value);
        }

        [Theory]
        [InlineData("(Jimmy+2)=5", 1)]
        [InlineData("IF(3>2;10;5)", 10)]
        [InlineData("IF(3<2;10;5)", 5)]
        [InlineData("Jimmy*8/Billy", 6)]
        [InlineData("Jimmy*10/(Billy+1)", 6)]
        [InlineData("Billy/2/2*Jimmy*2", 6)]
        [InlineData("Billy/2/2*-Jimmy*2", -6)]
        [InlineData("Billy*Jimmy/-2", -6)]
        [InlineData("(Billy+Jimmy)*4/-Billy", -7)]
        [InlineData("Jimmy>-Billy", 1)]
        public void Test_Logic_Success(string expression, double result)
        {
            var engine = Create();

            _items["Jimmy"].Value = 3;
            _items["Billy"].Value = 4;
            var item = engine.Add("foo", expression);


            Assert.Equal(result, item.Value);
        }

        [Theory]
        [InlineData("2022-05-29 00:00:00", 5, 1)]
        [InlineData("2022-05-29 00:00:00", 30, 1)]
        [InlineData("2022-05-29 00:00:00", 30, 2)]
        [InlineData("2022-05-29 00:00:00", 60, 1)]
        [InlineData("2022-05-29 00:00:00", 60, 2)]
        [InlineData("2022-05-29 00:00:00", 60, 5)]
        [InlineData("2022-05-29 00:00:00", 3600, 1)]
        [InlineData("2022-05-29 00:00:00", 3600, 2)]
        public void Test_LastOf_Success(DateTime initialDT, int parameter1, int parameter2)
        {
            var engine = Create(initialDT);
            var item = engine.Add("item3", $"LASTOF(Jimmy;{parameter1};{parameter2})");

            var expected = 0.0;
            for (int i = 0; i < parameter1 * 3; i++)
            {
                _items["Jimmy"].Value = i + 1;
                engine.Iterate();

                if ((i + 0) % parameter1 == 0)
                    expected = i - parameter1 * (parameter2 - 1);

                if (expected < 0) expected = 0;



                Assert.Equal(expected, item.Value);
            }
        }

        private static IEnumerable<IItem> CreateItems()
        {
            return new List<IItem>() { new DataItem("Jimmy"), new DataItem("Billy") };
        }

        private Engine Create(DateTime? initialDT = null)
        {
            _items = CreateItems().ToDictionary(x => x.Name);
            var names = new List<string>() { "item" };
            TestTimeProvider? timeProvider = null;

            if (initialDT != null)
                timeProvider = new TestTimeProvider((DateTime)initialDT);

            return new Engine(_items.Values, timeProvider ?? null);
        }
    }
}
