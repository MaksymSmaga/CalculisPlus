using System;
using System.Collections.Generic;
using System.Linq;
using Calculis.Core;

namespace Calculis.Functions
{
    public class StdevFunction : TemporalFunction
    {
        public StdevFunction(IList<IValueItem> args) : base(args)
        {
            Name = "STDEV";
            Function = () =>
            {
                var avg = _cash.Select(x => x.Value).Sum() / _cash.Length;

                var sum = 0.0;
                for (int i = 0; i < _cash.Length; i++)
                    sum += Math.Pow(_cash[i].Value - avg, 2);

                return Math.Sqrt(sum / _cash.Length);
            };
        }
    }
}
