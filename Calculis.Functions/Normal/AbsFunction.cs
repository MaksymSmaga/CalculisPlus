using Calculis.Core;
using Calculis.Core.Items;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgumentsNumber(1)]
    internal sealed class AbsFunction : NormalFunction
    {
        //public override FunctionInfo Info { get; protected set; }

        public AbsFunction(IList<IValueItem> args) : base(args)
        {
            Function = () => Math.Abs(args[0].Value);
        }
    }
}
