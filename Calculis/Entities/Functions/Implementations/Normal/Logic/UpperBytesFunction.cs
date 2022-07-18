using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic
{
    [ArgsNum(1)]
    internal class UpperBytesFunction : NormalFunction
    {
        public UpperBytesFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return System.Convert.ToDouble(Math.Floor(args[0].Value / 256));
            };
        }
    }
}
