using Calculis.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Functions
{
    public class IifFunction : NormalFunction
    {
        public IifFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args[0].Value != 0 ? args[1].Value : args[2].Value;
            };
        }
    }
}
