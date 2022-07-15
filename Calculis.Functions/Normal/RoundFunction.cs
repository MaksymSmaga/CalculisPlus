using Calculis.Core;
using System;
using System.Collections.Generic;

namespace Calculis.Functions.Normal
{
    [ArgumentsNumber(MinNumber = 1, MaxNumber = 2)]
    internal sealed class RoundFunction : NormalFunction
    {
        public RoundFunction(IList<IValueItem> args) : base(args)
        {
            Function = () => Math.Round(args[0].Value, (int)(args.Count > 1 ? args[1].Value : 0));
        }
    }
}
