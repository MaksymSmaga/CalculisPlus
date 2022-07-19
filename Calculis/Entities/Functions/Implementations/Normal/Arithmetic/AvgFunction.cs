using System.Collections.Generic;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    internal sealed class AvgFunction : BaseNormalFunction
    {
        public AvgFunction(IList<IItem> args) : base(args)
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
