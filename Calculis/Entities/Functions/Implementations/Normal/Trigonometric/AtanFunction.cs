using Calculis.Core.Entities.Functions.Abstractions;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;


namespace Calculis.Core.Entities.Functions.Implementations.Normal.Trigonometric
{
    internal sealed class AtanFunction : TrigonometricFunction
    {
        public AtanFunction(IList<IItem> args) : base(args, (double x) => Math.Atan(x)) { }
    }
}
