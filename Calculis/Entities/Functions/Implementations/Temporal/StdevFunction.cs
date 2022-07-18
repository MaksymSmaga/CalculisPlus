using System;
using System.Collections.Generic;
using System.Linq;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem), 1)]
    internal sealed class StdevFunction : TemporalFunction
    {
        public StdevFunction(IList<IItem> args) : base(args)
        {
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
