using Calculis.Core;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    internal abstract class LogicFunction : NormalFunction
    {
        public LogicFunction(IList<IValueItem> args, Func<bool, double, bool> compare) : base(args)
        {
            Function = () =>
            {
                var result = Convert.ToBoolean(_args[0].Value);

                for (int i = 1; i < _args.Count; i++)
                    result = compare(result, _args[i].Value);

                return Convert.ToDouble(result);
            };
        }
    }

    internal sealed class AndFunction : LogicFunction
    {
        public AndFunction(IList<IValueItem> args) : base(args, (bool res, double y) => res & Convert.ToBoolean(y)) { }
    }
    internal sealed class OrFunction : LogicFunction
    {
        public OrFunction(IList<IValueItem> args) : base(args, (bool res, double y) => res | Convert.ToBoolean(y)) { }
    }
    internal sealed class XorFunction : LogicFunction
    {
        public XorFunction(IList<IValueItem> args) : base(args, (bool res, double y) => res ^ Convert.ToBoolean(y)) { }
    }

    [ArgumentsNumber(1)]
    internal class NotFunction : NormalFunction
    {
        public NotFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args[0].Value != 0 ? 0 : 1;
            };
        }
    }
}
