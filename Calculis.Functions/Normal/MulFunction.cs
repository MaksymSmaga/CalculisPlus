﻿using System.Collections.Generic;
using Calculis.Core;

namespace Calculis.Functions
{
    public class MulFunction : NormalFunction
    {
        public MulFunction(IList<IValueItem> args) : base(args)
        {
            Name = "MUL";
            Function = () =>
            {
                var result = _args[0].Value;

                for (var i = 1; i < _args.Count; i++)
                    result *= _args[i].Value;

                return result;
            };
        }
    }
}