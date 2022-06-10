using Calculis.Core;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Functions
{
    internal sealed class MinFunction : NormalFunction
    {
        public MinFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args.Min(x => x.Value);
            };
        }
    }
}
