using Calculis.Core;
using System;
using System.Collections.Generic;

namespace Calculis.Functions
{
    //TODO: cash optimization - separated class and more suitable structure for saving

    public class CashItem : IValue
    {
        public double Value { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }

    public abstract class TemporalFunction : FunctionBase
    {
        protected bool _isInitialized = false;
        protected CashItem[] _cash { get; private set; }
        protected DateTimeOffset Timestamp { get; private set; }

        public TemporalFunction(IList<IValueItem> args) : base(args)
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            var cashSize = Convert.ToInt32(_args[_args.Count - 1]);
            InitializeCash(cashSize);
        }

        protected void InitializeCash(int cashSize)
        {
            _cash = new CashItem[cashSize];

            for (var i = 0; i < cashSize; i++)
                _cash[i] = new CashItem();
        }

        public override void Update(DateTime dateTime)
        {
            for (int i = 1; i < _cash.Length; i++)
            {
                _cash[i - 1].Value = _cash[i].Value;
                _cash[i - 1].Timestamp = _cash[i].Timestamp;
            }
                
            _cash[_cash.Length - 1].Value = _args[0].Value;
            _cash[_cash.Length - 1].Timestamp = dateTime;

            Timestamp = dateTime;
            _isInitialized = true;
        }
    }
}
