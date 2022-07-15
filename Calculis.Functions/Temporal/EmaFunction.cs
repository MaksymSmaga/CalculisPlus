using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem), 0, 1)]
    internal sealed class EmaFunction : TemporalFunction
    {
        public EmaFunction(IList<IValueItem> args) : base(args)
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
