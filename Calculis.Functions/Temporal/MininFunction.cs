using Calculis.Core;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem))]
    internal class MininFunction : TemporalFunction
    {
        public MininFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash.Min(x => x.Value);
            };
        }
    }
}
