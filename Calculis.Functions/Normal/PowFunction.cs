using System;
using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public class PowFunction : NormalFunction
    {
        public PowFunction(IList<IValueItem> args) : base(args)
        {
            Function = () => Math.Pow(args[0].Value, args[1].Value);
        }
    }
}
