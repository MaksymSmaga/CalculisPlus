﻿using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    internal sealed class MinFunction : BaseNormalFunction
    {
        public MinFunction(IList<IItem> args) : base(args)
        {
            Function = () => args.Min(x => x.Value);
        }
    }
}
