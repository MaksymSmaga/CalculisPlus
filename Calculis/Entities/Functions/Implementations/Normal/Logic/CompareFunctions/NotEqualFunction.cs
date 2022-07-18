using Calculis.Core.Entities.Functions.Abstractions;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic.CompareFunctions
{
    internal sealed class NotEqualFunction : CompareFunction
    {
        public NotEqualFunction(IList<IItem> args) : 
            base(args, (double x, double y) => x != y) { }
    }
}
