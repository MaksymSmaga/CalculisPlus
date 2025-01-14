﻿using System.Collections.Generic;
using Calculis.Core;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Functions
{
    [ArgsNum(1)]
    internal sealed class PeFunction : BaseTemporalFunction
    {
        public PeFunction(IList<IItem> args) : base(args)
        {
            Function = () => _cash[1].Value == 1 && _cash[0].Value == 0 ? 1 : 0;
        }

        protected override sealed void Initialize()
        {
            InitializeCash(2);
        }
    }
}
