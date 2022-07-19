using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Functions
{
    [ArgsNum(2)]
    [ArgsType(1, typeof(ConstantItem), 1)]
    internal sealed class MininFunction : BaseTemporalFunction
    {
        public MininFunction(IList<IItem> args) : base(args)
        {
            Function = () => _cash.Min(x => x.Value); 
        }
    }
}
