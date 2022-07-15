using Calculis.Core;
using Calculis.Core.Calculation;
using Calculis.Test.Auxilliary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Calculis.Tests
{
    public class FunctionsTest
    {
        private IDictionary<string, IValueItem> _items { get; set; }

        [Theory]
        [InlineData(1.0, 4.0)]
        public void Test_Sum_Success(double x, double y)
        {
            var engine = Create();

            _items["bobby"].Value = x;
            _items["Billy"].Value = y;

            var item = engine.Add("pussy", "SUM(bobby;Billy)");


            Assert.Equal(x + y, item.Value);
        }

        [Theory]
        [InlineData(1.0, 4.0)]
        public void Test_Arithmetic_Success(double x, double y)
        {
            var engine = Create();

            _items["bobby"].Value = x;
            _items["Billy"].Value = y;

            var item = engine.Add("pussy", $"bobby+{0.3.ToString(CultureInfo.CurrentCulture.NumberFormat)}");


            Assert.Equal(1.3, item.Value);
        }

        [Theory]
        [InlineData("(bobby+2)=5", 1)]
        [InlineData("IF(3>2;10;5)", 10)]
        [InlineData("IF(3<2;10;5)", 5)]
        [InlineData("bobby*8/Billy", 6)]
        [InlineData("bobby*10/(Billy+1)", 6)]
        [InlineData("Billy/2/2*bobby*2", 6)]
        [InlineData("Billy/2/2*-bobby*2", -6)]
        [InlineData("Billy*bobby/-2", -6)]
        [InlineData("(Billy+bobby)*4/-Billy", -7)]
        [InlineData("bobby>-Billy", 1)]
        public void Test_Logic_Success(string expression, double result)
        {
            var engine = Create();


            _items["bobby"].Value = 3;
            _items["Billy"].Value = 4;
            var item = engine.Add("pussy", expression);


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
            var item = engine.Add("item3", $"LASTOF(bobby;{parameter1};{parameter2})");

            var expected = 0.0;
            for (int i = 0; i < parameter1 * 3; i++)
            {
                _items["bobby"].Value = i + 1;
                engine.Iterate();

                if((i + 0) % parameter1 == 0)
                    expected = i - parameter1 * (parameter2 - 1);
                
                if(expected < 0) expected = 0;



                Assert.Equal(expected, item.Value);
            }
        }

        private IEnumerable<IValueItem> CreateItems()
        {
            return new List<IValueItem>() { new DataItem("bobby"), new DataItem("Billy") };
        }

        private CalculisEngine Create(DateTime? initialDT = null)
        {
            _items = CreateItems().ToDictionary(x => x.Name);
            var names = new List<string>() { "item" };
            TestTimeProvider timeProvider = null;

            if (initialDT != null)
                timeProvider = new TestTimeProvider((DateTime)initialDT);

            return new CalculisEngine(_items.Values, timeProvider ?? null);
        }
    }
}
