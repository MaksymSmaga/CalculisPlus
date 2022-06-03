using Calculis.Core.Convert;
using System.Collections.Generic;
using Calculis.Core.Auxilliary;

namespace Calculis.Core.Calculation
{
    public sealed class CalculisEngine
    {
        private readonly ItemsManager _itemsManager;
        private readonly TimeProvider _timeProvider;

        ///<summary>
        ///Initializes a new instance of calculation engine that carry out real-time calculations based on items values
        ///</summary>
        ///<param name="Items">Collection of value contained objects inherited of IValueItem</param>
        ///<param name="TimeProvider">Provider of time for control of iteration in temporal functions;\nBy default is used standard provider based on System.DateTime</param>
        public CalculisEngine(IEnumerable<IValueItem> Items, TimeProvider TimeProvider = null)
        {
            _timeProvider = TimeProvider ?? new DefaultTimeProvider();
            _itemsManager = new ItemsManager(Items);
        }

        ///<summary>
        ///Adds new IValueItem object with formulary expression
        ///</summary>
        ///<param name="name">Name of the new object</param>
        ///<param name="expression">Formulary expression that will be used for calculation the value</param>
        ///<returns>CalculatingItem object with the result of calculation in Value field</returns>
        public CalculatingItem Add(string Name, string Expression)
        {
            return _itemsManager.Create(Name, Expression); ;
        }

        ///<summary>
        ///Pluggs-in an assembly containing additional functions 
        ///</summary>
        ///<param name="name">Name of assembly</param>
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
    }
}
