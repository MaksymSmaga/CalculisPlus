using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions.Base.FunctionTypes
{
    internal abstract class CompareFunction : NormalFunction
    {
        public CompareFunction(IList<IItem> args, Func<double, double, bool> compare) : base(args)
        {
            Function = () =>
            {
                var result = true;

                for (int i = 1; i < _args.Count; i++)
                    result = result && compare(_args[i - 1].Value, _args[i].Value);

                return System.Convert.ToDouble(result);
            };
        }
    }
}
