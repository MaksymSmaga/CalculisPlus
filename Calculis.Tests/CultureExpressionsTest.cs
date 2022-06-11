using Calculis.Tests.Auxilliary;
using System.Globalization;
using Xunit;

namespace Calculis.Tests
{
    public class CultureExpressionsTest
    {
        private static double[] _values = new[] { -5.0, 4.0 };

        private static TheoryData<CultureInfo> PrepareParams()
        {
            var param_s = new TheoryData<CultureInfo>();

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
                param_s.Add(culture);

            return param_s;
        }

        [Theory]
        [MemberData(nameof(PrepareParams))]
        public void Test_Sum_Success(CultureInfo culture)
        {
            var engine = CalculisFactory.Create(_values);


            var item = engine.Add("result", $"i1*i2*{0.345.ToString(culture)}", culture);


            Assert.Equal(-5 * 4 * 0.345, item.Value);
        }
    }
}
