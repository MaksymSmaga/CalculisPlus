using Calculis.Core.Entities.Functions.Abstractions.Base;
using Calculis.Core.Entities.Functions.Abstractions.Types;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Items.Implementations
{
    /// <summary>
    /// CalcItem entity contains Name and Value 
    /// what calculatinig from a function-argument and updating data-arguments.
    /// </summary>
    public sealed class CalcItem : IItem
    {
        /// <summary>
        /// Pivate field to get function as argument of the constructor CalcItem. 
        /// </summary>
        private readonly BaseFunction _function;

        /// <summary>
        /// Checks if private field _function is compatible with BaseTemporalFunction type. 
        /// </summary>
        internal bool IsTemporal => _function is BaseTemporalFunction;

        /// <summary>
        /// Name property of CalcItem entity. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value to calculate the result of the _function. 
        /// </summary>
        public double Value
        {
            get { return _function.Function(); }
            set { }
        }

        /// <summary>
        /// Constructor - CalcItem entity gets function to calculate Value. 
        /// </summary>
        public CalcItem(BaseFunction function)
        {
            _function = function;
        }

        /// <summary>
        /// Updates data for the _function.
        /// </summary>
        /// <param name="sender">object that fired the event.</param>
        /// <param name="args">a class that makes it possible to pass
        /// additional information to the handler from UpdateArgs args.</param>
        internal void Update(object sender, UpdateArgs args)
        {
            _function.Update(args.TimeStamp);
        }

        ///<summary>
        ///Initializes cash of items based on temporal function.
        ///</summary>
        ///<param name="CashValues">Content for initialization of cash</param>
        ///<exception>InvalidOperationException</exception>
        public void Initialize(IEnumerable<IValue> values)
        {
            if (!IsTemporal)
            {
                throw new InvalidOperationException("Non temporal function cannot be initialized!");
            }

            ((BaseTemporalFunction)_function).Initialize(values);
        }
    }
}
