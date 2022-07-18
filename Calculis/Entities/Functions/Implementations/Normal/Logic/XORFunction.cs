using Calculis.Core.Entities.Functions.Abstractions;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Implementations.Normal.Logic
{
    internal sealed class XORFunction : LogicFunction
    {
        public XORFunction(IList<IItem> args) :
            base(args, (res, y) => res ^ System.Convert.ToBoolean(y))
        { }
    }
}
