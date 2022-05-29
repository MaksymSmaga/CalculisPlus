using System;

namespace Calculis.Core.Auxilliary
{
    internal class DefaultTimeProvider : TimeProvider
    {
        private DateTime _dateTime;

        public DefaultTimeProvider()
        {
            Update();
        }

        public override DateTime Now
        {
            get { return _dateTime; }
        }

        public override void Update()
        {
            _dateTime = DateTime.Now;
        }
    }
}
