using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgsNum(1)]
    internal sealed class IntegrFunction : TemporalFunction
    {
        public IntegrFunction(IList<IItem> args) : base(args)
        {
            Function = () =>  args[0].Value + _previousValue;
        }

        protected override void Initialize()
        {
            InitializeCash(1);
        }
    }
}
