using Calculis.Core;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    internal abstract class CompareFunction : NormalFunction
    {
        public CompareFunction(IList<IValueItem> args, Func<double, double, bool> compare) : base(args)
        {
            Function = () =>
            {
                var result = true;

                for (int i = 1; i < _args.Count; i++)
                    result = result && compare(_args[i-1].Value, _args[i].Value);

                return Convert.ToDouble(result);
            };
        }
    }

    internal sealed class MoreFunction : CompareFunction
    {
        public MoreFunction(IList<IValueItem> args) : base(args, (double x, double y) => x > y) { }
    }

    internal sealed class MoreeqFunction : CompareFunction
    {
        public MoreeqFunction(IList<IValueItem> args) : base(args, (double x, double y) => x >= y) { }
    }

    internal sealed class LessFunction : CompareFunction
    {
        public LessFunction(IList<IValueItem> args) : base(args, (double x, double y) => x < y) { }
    }

    internal sealed class LesseqFunction : CompareFunction
    {
        public LesseqFunction(IList<IValueItem> args) : base(args, (double x, double y) => x <= y) { }
    }

    internal sealed class EqFunction : CompareFunction
    {
        public EqFunction(IList<IValueItem> args) : base(args, (double x, double y) => x == y) { }
    }

    internal sealed class NeqFunction : CompareFunction
    {
        public NeqFunction(IList<IValueItem> args) : base(args, (double x, double y) => x != y) { }
    }
}
