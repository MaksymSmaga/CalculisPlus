using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    internal sealed class MaxFunction : BaseNormalFunction
    {
        public MaxFunction(IList<IItem> args) : base(args)
        {
            Function = () => args.Max(x => x.Value);
        }
    }
}
