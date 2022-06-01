using System;
using System.Collections.Generic;

namespace Calculis.Core
{
    public abstract class FunctionBase
    {
        protected IList<IValueItem> _args;

        public FunctionBase(IList<IValueItem> args)
        {
            _args = args;
            Name = GetType().Name.Replace("Function", "").ToUpper();
        }

        public string Name { get; set; }
        public string Description { get; }
        public virtual Func<double> Function { get; protected set; }

        public virtual void Update(DateTime dateTime)
        {
            return;
        }
    }
}
