﻿using System;
using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public class SqrtFunction : NormalFunction
    {
        public SqrtFunction(IList<IValueItem> args) : base(args)
        {
            Name = "SQRT";
            Function = () => Math.Sqrt(args[0].Value);
        }
    }
}