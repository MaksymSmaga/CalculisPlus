using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Trigonometric
{
    [ArgumentsNumber(1)]
    internal sealed class PiFunction : NormalFunction
    {
        public PiFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.PI;
        }
    }
}
