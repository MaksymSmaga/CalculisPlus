using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public sealed class DelayFunction : TemporalFunction
    {
        public DelayFunction(IList<IValueItem> args) : base(args)
        {
            Name = "DELAY";
            Function = () =>
            {
                return _cash[_cash.Length - (int)args[1].Value].Value;
            };
        }
    }
}
