using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    [ArgsNum(1)]
    internal sealed class AbsFunction : BaseNormalFunction
    {
        public AbsFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.Abs(args[0].Value);
        }
    }
}
