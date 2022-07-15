using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
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
    [ArgumentsNumber(2)]
    internal class BitFunction : NormalFunction
    {
        public BitFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                var serviceValue = (int)Math.Pow(2, args[1].Value);
                return Convert.ToDouble((((int)args[0].Value) & serviceValue) == serviceValue);
            };
        }
    }
    [ArgumentsNumber(1)]
    internal class LowbyteFunction : NormalFunction
    {
        public LowbyteFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return args[0].Value - Convert.ToDouble(Math.Floor(args[0].Value / 256)) * 256;
            };
        }
    }
    [ArgumentsNumber(1)]
    internal class HighbyteFunction : NormalFunction
    {
        public HighbyteFunction(IList<IValueItem> args) : base(args)
        {
            Function = () =>
            {
                return Convert.ToDouble(Math.Floor(args[0].Value / 256));
            };
        }
    }
}
