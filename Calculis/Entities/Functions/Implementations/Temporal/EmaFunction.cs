using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgsNum(2)]
    [ArgsType(1, typeof(ConstantItem), 0, 1)]
    internal sealed class EmaFunction : BaseTemporalFunction
    {
        public EmaFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return args[1].Value * args[0].Value + (1 - args[1].Value) * _previousValue;
            };
        }

        protected override void Initialize()
        {
            InitializeCash(2);
        }
    }
}
