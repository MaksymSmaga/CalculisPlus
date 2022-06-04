using Calculis.Tests.Auxilliary;
using System;
using System.Reflection;
using Xunit;

namespace Calculis.Tests
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
