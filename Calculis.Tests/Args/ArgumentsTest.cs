using Calculis.Tests.Auxilliary;
using System.Reflection;
using Xunit;

namespace Calculis.Tests.Args
{
    public class ArgumentsTest
    {
        [Theory]
        [InlineData("DELAY(i1)")]
        [InlineData("DELAY(i1;10;20)")]
        [InlineData("LASTOF(i1)")]
        [InlineData("LASTOF(i1;10;20;12)")]
        [InlineData("MININ(i1)")]
        [InlineData("MININ(i1;1;2)")]
        [InlineData("MAXIN(i1)")]
        [InlineData("MAXIN(i1;2;3)")]
        [InlineData("STDEV(i1)")]
        [InlineData("STDEV(i1;1;2)")]
        [InlineData("INTEGR(i1;1)")]
        [InlineData("SIN(i1;1)")]
        [InlineData("SINH(i1;1)")]
        [InlineData("ASIN(i1;1)")]
        [InlineData("COS(i1;1)")]
        [InlineData("COSH(i1;1)")]
        [InlineData("ACOS(i1;1)")]
        [InlineData("TAN(i1;1)")]
        [InlineData("TANH(i1;1)")]
        [InlineData("ATAN(i1;1)")]
        [InlineData("LN(i1;1)")]
        [InlineData("EXP(i1;1)")]
        [InlineData("TRUNC(i1;1)")]
        [InlineData("ROUND(i1;1;2)")]
        [InlineData("IF(i1)")]
        [InlineData("IF(i1;1;2;3)")]
        [InlineData("BIT(i1)")]
        [InlineData("BIT(i1;1;2)")]
        [InlineData("LOWERBYTES(i1;1)")]
        [InlineData("UPPERBYTES(i1;1)")]
        public void Number_of_arguments_are_not_correct(string expression)
        {
            var engine = CalculisFactory.Create(new double[] { 0, 1 });

            var func = () => engine.Add("result", expression);

            Assert.Throws<TargetInvocationException>(func);
        }

        [Theory]
        [InlineData("DELAY(i1;i2)")]
        [InlineData("LASTOF(i1;i2)")]
        [InlineData("MININ(i1;i2)")]
        [InlineData("MAXIN(i1;i2)")]
        [InlineData("STDEV(i1;i2)")]
        public void Type_of_arguments_are_not_correct(string expression)
        {
            var engine = CalculisFactory.Create(new double[] { 0, 1 });

            var func = () => engine.Add("result", expression);

            Assert.Throws<TargetInvocationException>(func);
        }
    }
}
