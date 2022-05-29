using System.Collections.Generic;
using System.Linq;
using Calculis.Core;

namespace Calculis.Functions
{
    public class SmaFunction : TemporalFunction
    {
        public SmaFunction(IList<IValueItem> args) : base(args)
        {
            Name = "SMA";
            Function = () =>
            {
                return _cash.Select(x => x.Value).Sum();
            };
        }
    }
}
