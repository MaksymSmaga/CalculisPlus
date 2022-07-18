﻿using System.Collections.Generic;
using System.Linq;
using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;

namespace Calculis.Functions
{
    [ArgsNum(2)]
    [ArgsType(1, typeof(ConstantItem), 1)]
    internal sealed class MaxinFunction : TemporalFunction
    {
        public MaxinFunction(IList<IItem> args) : base(args)
        {
            Function = () =>_cash.Max(x => x.Value);
        }
    }
}