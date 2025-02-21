﻿using System.Collections.Generic;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Arithmetic
{
    internal sealed class DifFunction : BaseNormalFunction
    {
        public DifFunction(IList<IItem> args) : base(args)
        {
            Function = () =>
            {
                var result = _args[0].Value;

                for (var i = 1; i < _args.Count; i++)
                    result -= _args[i].Value;

                return result;
            };
        }
    }
}
