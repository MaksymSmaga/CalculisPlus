using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    [ArgumentsType(1, typeof(ConstantItem), 0)]
    internal sealed class DelayFunction : TemporalFunction
    {
        public DelayFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return _cash[_cash.Length - (int)args[1].Value].Value;
            };
        }
    }
}
