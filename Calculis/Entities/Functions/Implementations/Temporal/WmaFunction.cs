using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgsNum(2)]
    [ArgsType(1, typeof(ConstantItem), 1)]
    internal sealed class WmaFunction : TemporalFunction
    {
        public WmaFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                int n = _cash.Length;
                double sum = 0;

                for (int i = 0; i < n; i++)
                    sum += (n - i) * _cash[n - i - 1].Value;

                return 2 * sum / (n * (n + 1));
            };
        }
    }
}
