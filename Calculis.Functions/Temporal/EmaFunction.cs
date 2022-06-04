using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    internal class EmaFunction : TemporalFunction
    {
        public EmaFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                double alpha = args[1].Value;
                double ema_t = _cash[0].Value;

                for (int i = 1; i < args.Count; i++)
                    ema_t = alpha * args[i].Value + (1 - alpha) * ema_t;

                return ema_t;
            };
        }
    }
}
