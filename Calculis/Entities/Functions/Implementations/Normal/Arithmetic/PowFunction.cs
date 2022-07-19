using System;
using System.Collections.Generic;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    [ArgsNum(2)]
    internal sealed class PowFunction : BaseNormalFunction
    {
        public PowFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.Pow(args[0].Value, args[1].Value);
        }
    }
}
