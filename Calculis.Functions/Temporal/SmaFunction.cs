using System.Collections.Generic;
using System.Linq;
using Calculis.Core;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    internal class SmaFunction : TemporalFunction
    {
        public SmaFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash.Select(x => x.Value).Average();
            };
        }
    }
}
