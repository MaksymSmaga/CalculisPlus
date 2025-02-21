﻿using System.Collections.Generic;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    [ArgsNum(MinNumber = 1)]
    internal sealed class SumFunction : BaseNormalFunction
    {
        public SumFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                var sum = 0.0;

                foreach (var value in _args)
                    sum += value.Value;

                return sum;
            };
        }
    }
}
