using System;
using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    internal sealed class PowFunction : NormalFunction
    {
        public PowFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.Pow(args[0].Value, args[1].Value);
        }
    }
}
