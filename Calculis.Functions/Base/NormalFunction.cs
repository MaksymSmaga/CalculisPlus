using Calculis.Core;
using System.Collections.Generic;


namespace Calculis.Functions
{

    public abstract class NormalFunction : FunctionBase
    {
        public NormalFunction(IList<IValueItem> args) : base(args) { }
    }
}
