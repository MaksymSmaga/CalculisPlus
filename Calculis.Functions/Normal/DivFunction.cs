﻿using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Functions
{
    internal sealed class DivFunction : NormalFunction
    {
        public DivFunction(IList<IItem> args) : base(args)
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
