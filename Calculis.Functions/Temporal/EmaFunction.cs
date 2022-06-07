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
                //double alpha = args[1].Value;
                //double ema_t = _cash[0].Value;
                //for (int i = 1; i < args.Count; i++)
                //ema_t = alpha * args[0].Value + (1 - alpha) * _cash[0].Value;

                return args[1].Value * args[0].Value + (1 - args[1].Value) * _cash[1].Value;
            };
        }

        protected override void Initialize()
        {
            InitializeCash(2);
        }
    }
}
