using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    internal class DivFunction : NormalFunction
    {
        public DivFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                var result = _args[0].Value;

                for (var i = 1; i < _args.Count; i++)
                    result /= _args[i].Value;

                return result;
            };
        }
    }
}
