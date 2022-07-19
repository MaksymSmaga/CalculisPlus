using Calculis.Core.Entities.Functions.Abstractions.Base;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions.Types
{
    public abstract class BaseNormalFunction : BaseFunction
    {
        public BaseNormalFunction(IList<IItem> args) : base(args) { }
    }
}
