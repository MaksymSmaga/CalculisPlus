using Calculis.Core.Auxilliary;
using Calculis.Core.Entities.Functions.Abstractions;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Items.Implementations
{
    public sealed class CalculatingItem : IValueItem
    {
        internal bool IsTemporal => _function is TemporalFunction;

        private FunctionBase _function;

        public CalculatingItem(FunctionBase function)
        {
            _function = function;
        }

        public string Name { get; set; }
        public double Value { get { return _function.Function(); } set { } }

        internal void Update(object sender, UpdateArgs args)
        {
            _function.Update(args.Timestamp);
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
