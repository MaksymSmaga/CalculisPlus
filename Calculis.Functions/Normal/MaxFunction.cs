using Calculis.Core;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Functions
{
    internal class MaxFunction : NormalFunction
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
