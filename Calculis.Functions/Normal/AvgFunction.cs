using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public class AvgFunction : NormalFunction
    {
        public AvgFunction(IList<IValueItem> args) : base(args)
        {
            Name = "AVG";
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
