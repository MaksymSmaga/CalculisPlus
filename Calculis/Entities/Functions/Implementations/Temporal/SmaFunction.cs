using System.Collections.Generic;
using System.Linq;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem), 1)]
    internal sealed class SmaFunction : TemporalFunction
    {
        public SmaFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash.Select(x => x.Value).Average();
            };
        }
    }
}
