using System;
using System.Collections.Generic;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    [ArgsNum(1)]
    internal sealed class SqrtFunction : NormalFunction
    {
        public SqrtFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.Sqrt(args[0].Value);
        }
    }
}
