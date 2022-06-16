using Calculis.Test.Auxilliary;
using Calculis.Tests.Auxilliary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calculis.Tests
{
    public class HintTest
    {

        [Theory]
        [InlineData("elem,it1,it2,i1", "SUM(i", null, new [] { "it1", "it2", "i1" })]
        [InlineData("elem,it1,it2,i1", "SUM(it", null, new [] { "it1", "it2" })]
        [InlineData("elem,it1,it2,i1", "SUM(it;it2)", 5, new [] { "it1", "it2" })]
        [InlineData("elem,ele,it1,it2,i1", "SUM(el;it2)", 5, new [] { "elem", "ele" })]
        [InlineData("elem,ele,it1,it2,i1", "5+it", null, new [] { "it1", "it2" })]
        public void GetHint_test_succeed(string itemsNames, string expression, int? position, string[] result)
        {
            var names = itemsNames.Split(',');
            var engine = CalculisFactory.Create(names);


            var items = engine.GetHint(expression, position);


            Assert.Equal(items, result);
        }
    }
}
