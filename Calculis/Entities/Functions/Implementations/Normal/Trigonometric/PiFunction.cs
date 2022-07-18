using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Trigonometric
{
    [ArgsNum(1)]
    internal sealed class PiFunction : NormalFunction
    {
        public PiFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.PI;
        }
    }
}
