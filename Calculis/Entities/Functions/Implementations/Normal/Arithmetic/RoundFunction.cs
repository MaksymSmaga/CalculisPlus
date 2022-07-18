using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    [ArgumentsNumber(MinNumber = 1, MaxNumber = 2)]
    internal sealed class RoundFunction : NormalFunction
    {
        public RoundFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.Round(args[0].Value, (int)(args.Count > 1 ? args[1].Value : 0));
        }
    }
}
