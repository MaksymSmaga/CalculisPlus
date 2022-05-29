using Calculis.Core;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    public class AbsFunction : NormalFunction
    {
        public AbsFunction(IList<IValueItem> args) : base(args)
        {
            Name = "ABS";
            Function = () => Math.Abs(args[0].Value);
        }
    }
}
