using Calculis.Core.Entities.Functions.Abstractions.Base;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.TimeProviders;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Items.Implementations
{
    public sealed class CalcItem : IItem
    {
        internal bool IsTemporal => _function is TemporalFunction;

        private readonly FunctionBase _function;
        public string Name { get; set; }
        public double Value
        {
            get { return _function.Function(); }
            set { }
        }

        public CalcItem(FunctionBase function)
        {
            _function = function;
        }

        internal void Update(object sender, UpdateArgs args)
        {
            _function.Update(args.TimeStamp);
        }

        ///<summary>
        ///Initializes cash of item based on temporal function
        ///</summary>
        ///<param name="CashValues">Content for initialization of cash</param>
        ///<exception>InvalidOperationException</exception>
        public void Initialize(IEnumerable<IValue> values)
        {
            if (!IsTemporal)
            {
                throw new InvalidOperationException("Non temporal function cannot be initialized!");
            }

            ((TemporalFunction)_function).Initialize(values);
        }
    }
}
