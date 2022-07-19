using Calculis.Core.Convert;
using System.Collections.Generic;
using System.Globalization;
using System;
using Calculis.Core.Entities.Items.Abstractions;
using Calculis.Core.Entities.Items.Implementations;
using Calculis.Core.Entities.TimeProviders.Abstractions;
using Calculis.Core.Entities.TimeProviders.Implementations;
using Calculis.Core.Entities.Functions;

namespace Calculis.Core.Calculation
{
    public sealed class Engine
    {
        private readonly ItemsManager _itemsManager;
        private readonly BaseTimeProvider _timeProvider;

        ///<summary>
        ///Initializes a new instance of calculation engine that carry out real-time calculations based on items values
        ///</summary>
        ///<param name="Items">Collection of value contained objects inherited of IItem</param>
        ///<param name="TimeProvider">Provider of time for control of iteration in temporal functions;\nBy default is used standard provider based on System.DateTime</param>
        public Engine(IEnumerable<IItem> Items, BaseTimeProvider TimeProvider = null)
        {
            _timeProvider = TimeProvider ?? new TimeProvider();
            _itemsManager = new ItemsManager(Items);
        }

        ///<summary>
        ///Adds new IItem object with formulary expression
        ///</summary>
        ///<param name="Name">Name of the new object</param>
        ///<param name="Expression">Formulary expression that will be used for calculation the value</param>
        ///<param name="Culture">Object CultureInfo specifies the culture for Expression</param>
        ///<returns>CalcItem object with the result of calculation in Value field</returns>
        public CalcItem Add(string Name, string Expression, CultureInfo Culture = null)
        {
            return _itemsManager.Create(Name, Expression, Culture ?? CultureInfo.CurrentCulture);
        }

        ///<summary>
        ///Initializes cash of item based on temporal function
        ///</summary>
        ///<param name="Name">Name of the initialized object</param>
        ///<param name="CashValues">Content for initialization of cash</param>
        ///<exception>InvalidOperationException</exception>
        ///<exception>ArgumentException</exception>
        public void Initialize(string Name, IEnumerable<IValue> CashValues)
        {
            var item = _itemsManager.GetItem(Name);

            var calc = item as CalcItem;

            if (calc?.IsTemporal != true)
            {
                throw new InvalidOperationException("Non temporal function cannot be initialized!");
            }

            calc.Initialize(CashValues);
        }

        ///<summary>
        ///Returns IItem contained in Calculis instance
        ///</summary>
        ///<param name="Name">Name of the object</param>
        ///<returns>The object implemented IItem</returns>
        public IItem GetItem(string Name)
        {
            return _itemsManager.GetItem(Name); ;
        }

        ///<summary>
        ///Pluggs-in an assembly containing additional functions 
        ///</summary>
        ///<param name="AssemblyName">Name of assembly</param>
        public void Register(string AssemblyName)
        {
            FunctionManager.Register(AssemblyName);
        }

        ///<summary>
        ///Signals the expiration of a discrete period of time, initiates the updating of the cache of temporal functions
        ///</summary>
        public void Iterate()
        {
            _timeProvider?.Update();
            _itemsManager.Update(_timeProvider.Now);
        }

        ///<summary>
        ///Returns hint about allowed arguments depending on Expression context 
        ///</summary>
        ///<param name="Expression">Typed string of symbols for futher elemnts' expression</param>
        ///<param name="Position">Cursor position in Expression</param>
        public ICollection<string> GetHint(string Expression, int? Position = null)
        {
            if (Expression == null)
            {
                throw new ArgumentNullException(Expression);
            }

            var position = Position ?? Expression.Length - 1;
            return _itemsManager.GetHint(Expression, position);
        }
    }
}
