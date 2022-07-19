using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgsNum(2)]
    [ArgsType(1, typeof(ConstantItem), 0)]
    internal sealed class DelayFunction : BaseTemporalFunction
    {
        public DelayFunction(IList<IItem> args) : base(args)
        {
            Function = () => _cash[_cash.Length - (int)args[1].Value].Value;
        }
    }
}
