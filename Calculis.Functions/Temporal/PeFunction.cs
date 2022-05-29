using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public sealed class PeFunction : TemporalFunction
    {
        public PeFunction(IList<IValueItem> args) : base(args)
        {
            Name = "PE";
            Function = () =>
            {
                return _cash[1].Value == 1 && _cash[0].Value == 0 ? 1 : 0;
            };
        }

        protected override sealed void Initialize()
        {
            InitializeCash(2);
        }
    }
}
