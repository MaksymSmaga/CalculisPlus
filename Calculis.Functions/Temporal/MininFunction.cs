using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem), 1)]
    internal sealed class MininFunction : TemporalFunction
    {
        public MininFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash.Min(x => x.Value);
            };
        }
    }
}
