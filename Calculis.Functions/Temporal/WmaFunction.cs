using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    internal class WmaFunction : TemporalFunction
    {
        public WmaFunction(IList<IValueItem> args) : base(args)
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
