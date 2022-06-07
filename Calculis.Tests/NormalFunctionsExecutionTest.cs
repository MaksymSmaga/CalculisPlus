using Calculis.Tests.Auxilliary;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Calculis.Tests
{
    public class NormalFunctionsExecutionTest
    {
        private static double[] _values = new[] { -5.0, 4.0, 3.0, 2.0, 7.0 };
        private static IDictionary<string, Func<double[], double>> _functions = new Dictionary<string, Func<double[], double>>();

        static NormalFunctionsExecutionTest()
        {
            _functions.Add("SUM(i1;i2)", (double[] args) => args[0] + args[1]);
            _functions.Add("DIF(i1;i2)", (double[] args) => args[0] - args[1]);
            _functions.Add("MUL(i1;i2)", (double[] args) => args[0] * args[1]);
            _functions.Add("DIV(i1;i2)", (double[] args) => args[0] / args[1]);
            _functions.Add("POW(i1;i2)", (double[] args) => Math.Pow(args[0], args[1]));
            _functions.Add("SQRT(i2)", (double[] args) => Math.Sqrt(args[1]));
            _functions.Add("AVG(i1;i2;i3;i4;i5)", (double[] args) => args.Average());
            _functions.Add("ABS(i1)", (double[] args) => Math.Abs(args[0]));
            _functions.Add("MAX(i1;i2;i3;i4;i5)", (double[] args) => args.Max());
            _functions.Add("MIN(i1;i2;i3;i4;i5)", (double[] args) => args.Min());
            _functions.Add("IF(i1 > i2;10;20)", (double[] args) => args[0] > args[1] ? 10 : 20);
            _functions.Add("i1+i2", (double[] args) => args[0] + args[1]);
            _functions.Add("i1-i2", (double[] args) => args[0] - args[1]);
            _functions.Add("i1*i2", (double[] args) => args[0] * args[1]);
            _functions.Add("i1/i2", (double[] args) => args[0] / args[1]);
            _functions.Add("i1>i2", (double[] args) => args[0] > args[1] ? 1 : 0);
            _functions.Add("i1>=i2", (double[] args) => args[0] >= args[1] ? 1 : 0);
            _functions.Add("i1<i2", (double[] args) => args[0] < args[1] ? 1 : 0);
            _functions.Add("i1<=i2", (double[] args) => args[0] <= args[1] ? 1 : 0);
            _functions.Add("i1=i2", (double[] args) => args[0] == args[1] ? 1 : 0);
            _functions.Add("i1<>i2", (double[] args) => args[0] != args[1] ? 1 : 0);
        }
        private static TheoryData<string, double[], double> PrepareParams()
        {
            var param_s = new TheoryData<string, double[], double>();

            foreach (var func in _functions)
                param_s.Add(func.Key, _values, func.Value(_values));

            return param_s;
        }

        [Theory]
        [MemberData(nameof(PrepareParams))]
        public void Normal_function_with_simple_args_succeed(string expression, double[] args, double result)
        {
            var engine = CalculisFactory.Create(args);


            var item = engine.Add("result", expression);


            Assert.Equal(result, item.Value);
        }

        [Theory]
        [MemberData(nameof(PrepareParams))]
        public void Normal_function_with_simple_args_failed(string expression, double[] args, double result)
        {
            var engine = CalculisFactory.Create(args);


            var item = engine.Add("result", expression);


            Assert.NotEqual(result + 1, item.Value);
        }
    }
}
