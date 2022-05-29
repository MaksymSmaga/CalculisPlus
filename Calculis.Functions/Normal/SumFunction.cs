using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public class SumFunction : NormalFunction
    {
        public SumFunction(IList<IValueItem> args) : base(args)
        {
            Name = "SUM";
            Function = () =>
            {
                var sum = 0.0;

                foreach (var value in _args)
                    sum += value.Value;

                return sum;
            };
        }
    }
}
