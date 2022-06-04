using System.Collections.Generic;
using System.Linq;
using Calculis.Core;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem))]
    internal class MaxinFunction : TemporalFunction
    {
        public MaxinFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash.Max(x => x.Value);
            };
        }
    }
}
