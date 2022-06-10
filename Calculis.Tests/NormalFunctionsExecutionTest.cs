using Calculis.Tests.Auxilliary;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            _functions.Add("NOT(i1)", (double[] args) => args[0] != 0.0 ? 0 : 1);
            _functions.Add("AND(i1;1;1)", (double[] args) => 1);
            _functions.Add("AND(i1;1;0)", (double[] args) => 0);
            _functions.Add("OR(i1;0;0)", (double[] args) => 1);
            _functions.Add("XOR(i1;0;0)", (double[] args) => 1);
            _functions.Add("XOR(i1;0;1)", (double[] args) => 0);
            _functions.Add("PI(1)", (double[] args) => Math.PI);
            _functions.Add("SIN(i1)", (double[] args) => Math.Sin(args[0]));
            _functions.Add("SINH(i1)", (double[] args) => Math.Sinh(args[0]));
            //_functions.Add("ASIN(0,95)", (double[] args) => Math.Asin(0.95));
            _functions.Add("COS(i1)", (double[] args) => Math.Cos(args[0]));
            _functions.Add("COSH(i1)", (double[] args) => Math.Cosh(args[0]));
            //_functions.Add("ACOS(0,95)", (double[] args) => Math.Acos(0.95));
            _functions.Add("TAN(i1)", (double[] args) => Math.Tan(args[0]));
            _functions.Add("TANH(i1)", (double[] args) => Math.Tanh(args[0]));
            _functions.Add("ATAN(i1)", (double[] args) => Math.Atan(args[0]));
            _functions.Add("EXP(i1)", (double[] args) => Math.Exp(args[0]));
            //_functions.Add("LN(0,95)", (double[] args) => Math.Log(0.95));
            //_functions.Add("LOG(0,95)", (double[] args) => Math.Log10(0.95));
            _functions.Add($"LOG({0.95.ToString(CultureInfo.CurrentCulture.NumberFormat)};2)", (double[] args) => Math.Log(0.95, 2));
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
