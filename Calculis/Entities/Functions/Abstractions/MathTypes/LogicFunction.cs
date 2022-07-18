using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions.Base.FunctionTypes
{
    internal abstract class LogicFunction : NormalFunction
    {
        public LogicFunction(IList<IItem> args, Func<bool, double, bool> compare) : base(args)
        {
            Function = () =>
            {
                var result = System.Convert.ToBoolean(_args[0].Value);

                for (int i = 1; i < _args.Count; i++)
                    result = compare(result, _args[i].Value);

                return System.Convert.ToDouble(result);
            };
        }
    }
}
