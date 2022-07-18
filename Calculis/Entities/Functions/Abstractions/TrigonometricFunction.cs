using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions
{
    [ArgumentsNumber(1)]
    internal abstract class TrigonometricFunction : NormalFunction
    {
        public TrigonometricFunction(IList<IItem> args, Func<double, double> calculate) : base(args)
        {
            Function = () => calculate(args[0].Value); 
        }
    }
}
