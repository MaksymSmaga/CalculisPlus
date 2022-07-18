using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgumentsNumber(1)]
    internal sealed class AbsFunction : NormalFunction
    {
        //public override FunctionInfo Info { get; protected set; }

        public AbsFunction(IList<IItem> args) : base(args)
        {
            Function = () => Math.Abs(args[0].Value);
        }
    }
}
