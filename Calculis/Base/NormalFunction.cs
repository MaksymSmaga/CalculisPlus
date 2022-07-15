using System.Collections.Generic;


namespace Calculis.Core
{

    public abstract class NormalFunction : FunctionBase
    {
        public NormalFunction(IList<IValueItem> args) : base(args) { }
    }
}
