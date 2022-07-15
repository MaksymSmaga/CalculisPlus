using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Functions
{
    [ArgumentsNumber(MinNumber = 1)]
    internal sealed class SumFunction : NormalFunction
    {
        public SumFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                var sum = 0.0;

                foreach (var value in _args)
                    sum += value.Value;

                return sum;
            };
        }
    }
}
