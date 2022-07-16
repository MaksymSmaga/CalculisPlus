﻿using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    [ArgumentsNumber(1)]
    internal abstract class TrigonometricFunction : NormalFunction
    {
        public TrigonometricFunction(IList<IItem> args, Func<double, double> calculate) : base(args)
        {
            Function = () =>
            {
                return calculate(args[0].Value);
            };
        }
    }
    internal sealed class SinFunction : TrigonometricFunction
    {
        public SinFunction(IList<IItem> args) : base(args, (double x) => Math.Sin(x)) { }
    }

    internal sealed class SinhFunction : TrigonometricFunction
    {
        public SinhFunction(IList<IItem> args) : base(args, (double x) => Math.Sinh(x)) { }
    }

    internal sealed class AsinFunction : TrigonometricFunction
    {
        public AsinFunction(IList<IItem> args) : base(args, (double x) => Math.Asin(x)) { }
    }

    internal sealed class CosFunction : TrigonometricFunction
    {
        public CosFunction(IList<IItem> args) : base(args, (double x) => Math.Cos(x)) { }
    }

    internal sealed class CoshFunction : TrigonometricFunction
    {
        public CoshFunction(IList<IItem> args) : base(args, (double x) => Math.Cosh(x)) { }
    }

    internal sealed class AcosFunction : TrigonometricFunction
    {
        public AcosFunction(IList<IItem> args) : base(args, (double x) => Math.Acos(x)) { }
    }

    internal sealed class TanFunction : TrigonometricFunction
    {
        public TanFunction(IList<IItem> args) : base(args, (double x) => Math.Tan(x)) { }
    }

    internal sealed class TanhFunction : TrigonometricFunction
    {
        public TanhFunction(IList<IItem> args) : base(args, (double x) => Math.Tanh(x)) { }
    }

    internal sealed class AtanFunction : TrigonometricFunction
    {
        public AtanFunction(IList<IItem> args) : base(args, (double x) => Math.Atan(x)) { }
    }

    internal sealed class LnFunction : TrigonometricFunction
    {
        public LnFunction(IList<IItem> args) : base(args, (double x) => Math.Log(x)) { }
    }

    internal sealed class ExpFunction : TrigonometricFunction
    {
        public ExpFunction(IList<IItem> args) : base(args, (double x) => Math.Exp(x)) { }
    }

    [ArgumentsNumber(1)]
    internal sealed class PiFunction : NormalFunction
    {
        public PiFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return Math.PI;
            };
        }
    }

    internal sealed class LogFunction : NormalFunction
    {
        public LogFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                return args.Count > 1 ? Math.Log(args[0].Value, args[1].Value) : Math.Log10(args[0].Value);
            };
        }
    }
}
