using Calculis.Core.Convert;
using Calculis.Core.Entities.Items;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;
using Xunit;

namespace Calculis.Tests
{
    public class ParserTest
    {
        private ExpressionParser _parser = new ExpressionParser(new ItemsManager(new List<IItem>()));

        [Theory]
        [InlineData("item1+item2-(item3-1+DIFF(item1;2))", "SUM(SUM(item1;item2);-SUM(SUM(item3;-1);DIFF(item1;2)))")]
        [InlineData("(item1+5)-(item2-item3)", "SUM(SUM(item1;5);-SUM(item2;-item3))")]
        [InlineData("(item1+1)-item2*item3", "SUM(SUM(item1;1);-MUL(item2;item3))")]
        [InlineData("item1+(1-item2)*item3", "SUM(item1;MUL(SUM(1;-item2);item3))")]
        [InlineData("SUM(item1+item2;2)*item3", "MUL(SUM(SUM(item1;item2);2);item3)")]
        [InlineData("SUM(item1+item2-5;15)+4*item3", "SUM(SUM(SUM(item1;item2;-5);15);MUL(4;item3))")]
        [InlineData("item1>item2*item3", "MUL(MORE(item1;item2);item3)")]
        [InlineData("item1<=item2*item3", "MUL(LESSEQ(item1;item2);item3)")]
        [InlineData("item1<=(item2*item3)", "LESSEQ(item1;MUL(item2;item3))")]
        [InlineData("item1+item2+item3)", "SUM(item1;item2;item3)")]
        [InlineData("item1*item2*item3)", "MUL(item1;item2;item3)")]
        [InlineData("item1*item2*item3/item4)", "MUL(item1;item2;item3;/item4)")]
        [InlineData("item1/2/2*-2*2", "MUL(item1;/2;/2;-2;2)")]
        public void Test_Parsing_Success(string original, string expected)
        {
            Assert.Equal(expected, _parser.ToFunctionView(original));
        }
    }
}
