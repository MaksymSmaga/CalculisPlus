using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    internal sealed class MinFunction : NormalFunction
    {
        public MinFunction(IList<IItem> args) : base(args)
        {
            Function = () => args.Min(x => x.Value);
        }
    }
}
