using Calculis.Core.Entities.Functions.Abstractions;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic
{
    internal sealed class ORFunction : LogicFunction
    {
        public ORFunction(IList<IItem> args) :
            base(args, (res, y) => res | System.Convert.ToBoolean(y))
        { }
    }
}
