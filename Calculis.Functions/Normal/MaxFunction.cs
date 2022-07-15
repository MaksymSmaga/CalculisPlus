using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Functions
{
    internal sealed class MaxFunction : NormalFunction
    {
        public MaxFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args.Max(x => x.Value);
            };
        }
    }
}
