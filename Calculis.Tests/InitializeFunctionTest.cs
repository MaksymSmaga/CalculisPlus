using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;
using Calculis.Tests.Auxilliary;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculis.Tests
{
    public class InitializeFunctionTest
    {

        [Fact]
        public void Try_to_initialize_non_temporal_failed()
        {
            var engine = CalculisFactory.Create(new double[] { 0, 1 });
            var args = new List<IValue>();
            args.Add(new CashItem { Value = 5 });
            args.Add(new CashItem { Value = 6 });


            engine.Add("abs", "ABS(i1)");


            Assert.Throws<InvalidOperationException>(() => engine.Initialize("i1", args));
            Assert.Throws<InvalidOperationException>(() => engine.Initialize("abs", args));
        }

        [Fact]
        public void Test_valid_number_of_args()
        {
            var engine = CalculisFactory.Create(new double[] { 0, 1 });
            var args = new List<IValue>();
            args.Add(new CashItem { Value = 5 });
            args.Add(new CashItem { Value = 6 });
            args.Add(new CashItem { Value = 7 });


            engine.Add("test-delay", "DELAY(i1;4)");

            //Non-valid
            Assert.Throws<ArgumentException>(() => engine.Initialize("test-delay", args));

            //Valid
            args.Add(new CashItem { Value = 5 });
            engine.Initialize("test-delay", args);
        }

        [Fact]
        public void Cash_engine_initialization_succeed()
        {
            var engine = CalculisFactory.Create(new double[] { 0 });
            var args = new List<IValue>();
            args.Add(new CashItem { Value = 5 });
            args.Add(new CashItem { Value = 6 });
            args.Add(new CashItem { Value = 7 });
            args.Add(new CashItem { Value = 8 });


            var calc = engine.Add("test-delay", "DELAY(i1;4)");
            engine.Initialize("test-delay", args);


            for(int i = 0; i < args.Count; i++)
            {
                Assert.Equal(args[i].Value, calc.Value);
                engine.Iterate();
            }
        }

        [Fact]
        public void Cash_item_initialization_succeed()
        {
            var engine = CalculisFactory.Create(new double[] { 0 });
            var args = new List<IValue>();
            args.Add(new CashItem { Value = 5 });
            args.Add(new CashItem { Value = 6 });
            args.Add(new CashItem { Value = 7 });
            args.Add(new CashItem { Value = 8 });


            var calc = engine.Add("test-delay", "DELAY(i1;4)");
            calc.Initialize(args);


            for (int i = 0; i < args.Count; i++)
            {
                Assert.Equal(args[i].Value, calc.Value);
                engine.Iterate();
            }
        }
    }
}
