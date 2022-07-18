using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Trigonometric
{
    internal sealed class LogFunction : NormalFunction
    {
        public LogFunction(IList<IItem> args) : base(args)
        {
            Function = () => args.Count > 1 ?
                Math.Log(args[0].Value, args[1].Value) : Math.Log10(args[0].Value);
        }
    }
}
