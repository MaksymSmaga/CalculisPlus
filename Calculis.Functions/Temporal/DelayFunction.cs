using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem))]
    public sealed class DelayFunction : TemporalFunction
    {
        public DelayFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash[_cash.Length - (int)args[1].Value].Value;
            };
        }
    }
}
