using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic
{
    [ArgumentsNumber(1)]
    internal class NOTFunction : NormalFunction
    {
        public NOTFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return args[0].Value != 0 ? 0 : 1;
            };
        }
    }
}
