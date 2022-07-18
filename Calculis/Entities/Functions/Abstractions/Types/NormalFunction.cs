using Calculis.Core.Entities.Functions.Abstractions.Base;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions.Types
{
    public abstract class NormalFunction : FunctionBase
    {
        public NormalFunction(IList<IItem> args) : base(args) { }
    }
}
