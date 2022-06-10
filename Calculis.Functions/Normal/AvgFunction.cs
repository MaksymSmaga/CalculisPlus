using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    internal sealed class AvgFunction : NormalFunction
    {
        public AvgFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                var sum = 0.0;

                foreach (var arg in args)
                    sum += arg.Value;

                return sum / args.Count;
            };
        }
    }
}
