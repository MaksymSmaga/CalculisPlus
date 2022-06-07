using Calculis.Core;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgumentsNumber(3)]
    internal class IfFunction : NormalFunction
    {
        public IfFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args[0].Value != 0 ? args[1].Value : args[2].Value;
            };
        }
    }
}
