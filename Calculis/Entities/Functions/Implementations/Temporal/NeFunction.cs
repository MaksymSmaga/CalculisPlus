using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Functions
{
    [ArgsNum(1)]
    internal sealed class NeFunction : TemporalFunction
    {
        public NeFunction(IList<IItem> args) : base(args)
        {
            Function = () => _cash[1].Value == 0 && _cash[0].Value == 1 ? 1 : 0;
        }

        protected override sealed void Initialize()
        {
            InitializeCash(2);
        }
    }
}
