using System;
using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Functions
{
    [ArgumentsNumber(1)]
    internal sealed class SqrtFunction : NormalFunction
    {
        public SqrtFunction(IList<IValueItem> args) : base(args)
        {
            Function = () => Math.Sqrt(args[0].Value);
        }
    }
}
