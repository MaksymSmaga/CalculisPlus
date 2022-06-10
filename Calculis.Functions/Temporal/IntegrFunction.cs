using Calculis.Core;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgumentsNumber(1)]
    internal sealed class IntegrFunction : TemporalFunction
    {
        public IntegrFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args[0].Value + _previousValue;
            };
        }

        protected override void Initialize()
        {
            InitializeCash(1);
        }
    }
}
