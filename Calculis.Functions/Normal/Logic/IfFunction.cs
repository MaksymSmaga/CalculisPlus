using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgumentsNumber(MinNumber = 2, MaxNumber = 3)]
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
