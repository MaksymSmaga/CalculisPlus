using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    [ArgumentsNumber(1)]
    internal sealed class NeFunction : TemporalFunction
    {
        public NeFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash[1].Value == 0 && _cash[0].Value == 1 ? 1 : 0;
            };
        }

        protected override sealed void Initialize()
        {
            InitializeCash(2);
        }
    }
}
