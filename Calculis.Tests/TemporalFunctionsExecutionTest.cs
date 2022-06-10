using Calculis.Tests.Auxilliary;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Calculis.Tests
{
    public class TemporalFunctionsExecutionTest
    {
        private static double[] _values = new[] { -5.0 };
        private static IDictionary<string, Func<int, double>> _functions = new Dictionary<string, Func<int, double>>();
        private static double _ema_prev = 0;
        private static double sum = 0;


        static TemporalFunctionsExecutionTest()
        {
            _functions.Add("DELAY(i1;30)", (int n) => HistoryManager.Value(n, 30));
            _functions.Add("MAXIN(i1;30)", (int n) => HistoryManager.Value(n - 1));
            _functions.Add("MININ(i1;30)", (int n) => HistoryManager.Value(n, 30));
            _functions.Add("INTEGR(i1)", (int n) => sum += HistoryManager.Value(n));
            _functions.Add("STDEV(i1;30)", (int n) => {
                var sum = 0.0;
                var pow = 0.0;
                for (int i = 0; i < 30; i++)
                    sum += HistoryManager.Value(n, i + 1);
                sum = sum / 30;
                for (int i = 0; i < 30; i++)
                    pow += Math.Pow(sum - HistoryManager.Value(n, i + 1), 2);

                return Math.Sqrt(pow / 30);
                });
            _functions.Add("SMA(i1;30)", (int n) => {
                var sum = 0.0;
                for (int i = 0; i < 30; i++)
                    sum += HistoryManager.Value(n, i + 1);

                return sum / 30;
            });
            _functions.Add("WMA(i1;30)", (int n) => {
                var sum = 0.0;
                for (int i = 0; i < 30; i++)
                    sum += (30 - i) * HistoryManager.Value(n, i + 1);

                return 2.0 * sum / (30 * (30 + 1));
            });
            _functions.Add($"EMA(i1;{0.5.ToString(CultureInfo.CurrentCulture.NumberFormat)})", (int n) => {
                _ema_prev = HistoryManager.Value(n) * 0.5 + (1 - 0.5) * _ema_prev;
                return  _ema_prev;
            });
        }
        private static TheoryData<string, double[], Func<int, double>> PrepareParams()
        {
            var param_s = new TheoryData<string, double[], Func<int, double>>();

            foreach (var func in _functions)
                param_s.Add(func.Key, _values, func.Value);

            return param_s;
        }

        [Theory]
        [MemberData(nameof(PrepareParams))]
        public void Temporal_function_with_simple_args_succeed(string expression, double[] args, Func<int, double> result)
        {
            var engine = CalculisFactory.Create(args);
            var i1 = engine.GetItem("i1");

            var item = engine.Add("result", expression);
            for (int i = 0; i < HistoryManager.HistorySize; i++)
            {
                i1.Value = HistoryManager.Value(i);
                var r = result(i);
                

                Assert.True(Math.Abs(r - item.Value) < 0.000001);
                engine.Iterate();
            }
        }
    }

    static class HistoryManager
    {
        internal static int HistorySize { get; private set; } = 1000;
        private readonly static double[] _history = new double[HistorySize];

        static HistoryManager()
        {
            for (int i = 0; i < HistorySize; i++)
                _history[i] = i * 10000;
        }

        public static double Value(int n = 0, int delay = 0)
        {
            return (n - delay) < 0 ? 0 : 
                   (n - delay) >= HistorySize ? 0 :
                   _history[n - delay];
        }
    }
}
