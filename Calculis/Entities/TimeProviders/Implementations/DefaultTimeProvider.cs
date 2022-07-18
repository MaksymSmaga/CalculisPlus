using Calculis.Core.Entities.TimeProviders.Abstractions;
using System;

namespace Calculis.Core.Entities.TimeProviders.Implementations
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
