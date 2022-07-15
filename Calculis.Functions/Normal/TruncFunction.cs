using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
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
