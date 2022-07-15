using Calculis.Core.Entities.Functions.Abstractions;
using Calculis.Core.Entities.Items.Abstractions;
using System.Collections.Generic;


namespace Calculis.Core
{

    public abstract class NormalFunction : FunctionBase
    {
        public NormalFunction(IList<IValueItem> args) : base(args) { }
    }
}
