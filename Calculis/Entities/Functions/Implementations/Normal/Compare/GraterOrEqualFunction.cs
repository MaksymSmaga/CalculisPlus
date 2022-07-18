﻿using Calculis.Core.Entities.Functions.Abstractions.Base.FunctionTypes;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic.CompareFunctions
{
    internal sealed class GraterOrEqualFunction : CompareFunction
    {
        public GraterOrEqualFunction(IList<IItem> args) : base(args, (double x, double y) => x >= y) { }
    }
}