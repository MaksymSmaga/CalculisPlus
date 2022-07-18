using Calculis.Core.Entities.Functions.Abstractions.Base.FunctionTypes;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Trigonometric
{
    internal sealed class LnFunction : TrigonometricFunction
    {
        public LnFunction(IList<IItem> args) : base(args, (double x) => Math.Log(x)) { }
    }
}
