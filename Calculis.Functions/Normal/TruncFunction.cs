using Calculis.Core;
using System;
using System.Collections.Generic;

namespace Calculis.Functions.Normal
{
    [ArgumentsNumber(1)]
    internal sealed class TruncFunction : NormalFunction
    {
        public TruncFunction(IList<IValueItem> args) : base(args)
        {
            Function = () => Math.Truncate(args[0].Value);
        }
    }
}
