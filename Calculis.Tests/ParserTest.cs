using Calculis.Core.Convert;
using Xunit;

namespace Calculis.Tests
{
    public class ParserTest
    {
        private ArithmeticParser _parser = new ArithmeticParser();

        [Theory]
        //[InlineData("item1+item2-(item3-1+DIFF(item1;2))", "SUM(item1;item2;-SUM(item3;-1;DIFF(item1;2)))")]
        //[InlineData("item1+5.5-(DIV(item2;3)+DIFF(item1;2)-1)", "SUM(item1;5.5;-SUM(DIV(item2;3);DIFF(item1;2);-1))")]
        [InlineData("item1+item2-(item3-1+DIFF(item1;2))", "SUM(SUM(item1;item2);-SUM(SUM(item3;-1);DIFF(item1;2)))")]
        [InlineData("(item1+5.5)-(item2-item3)", "SUM(SUM(item1;5.5);-SUM(item2;-item3))")]
        [InlineData("(item1+1)-item2*item3", "SUM(SUM(item1;1);-MUL(item2;item3))")]
        [InlineData("item1+(1-item2)*item3", "SUM(item1;MUL(SUM(1;-item2);item3))")]
        [InlineData("SUM(item1+item2;2)*item3", "MUL(SUM(SUM(item1;item2);2);item3)")]
        [InlineData("SUM(item1+item2-5;15)+4*item3", "SUM(SUM(SUM(item1;item2;-5);15);MUL(4;item3))")]
        [InlineData("item1>item2*item3", "MUL(MORE(item1;item2);item3)")]
        public void Test_Parsing_Success(string original, string expected)
        {
            Assert.Equal(expected, _parser.ToFunctionView(original));
        }
    }
}
