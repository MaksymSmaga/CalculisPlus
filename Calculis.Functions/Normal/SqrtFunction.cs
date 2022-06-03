using System;
using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    [ArgumentsNumber(2)]
    public class SqrtFunction : NormalFunction
    {
        public SqrtFunction(IList<IValueItem> args) : base(args)
        {
            Function = () => Math.Sqrt(args[0].Value);
        }
    }
}
