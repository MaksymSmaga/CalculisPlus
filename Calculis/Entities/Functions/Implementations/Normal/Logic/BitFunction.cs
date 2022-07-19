using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic
{
    [ArgsNum(2)]
    internal class BitFunction : BaseNormalFunction
    {
        public BitFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                var serviceValue = (int)Math.Pow(2, args[1].Value);
                return System.Convert.ToDouble(((int)args[0].Value & serviceValue) == serviceValue);
            };
        }
    }
}
