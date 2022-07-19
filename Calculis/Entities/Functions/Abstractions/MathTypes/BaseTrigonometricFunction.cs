using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions.Base.FunctionTypes
{
    [ArgsNum(1)]
    internal abstract class BaseTrigonometricFunction : BaseNormalFunction
    {
        public BaseTrigonometricFunction(IList<IItem> args, Func<double, double> calculate) : base(args)
        {
            Function = () => calculate(args[0].Value);
        }
    }
}
