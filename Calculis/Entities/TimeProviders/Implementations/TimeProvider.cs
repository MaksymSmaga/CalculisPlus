using Calculis.Core.Entities.TimeProviders.Abstractions;
using System;

namespace Calculis.Core.Entities.TimeProviders.Implementations
{
    internal class TimeProvider : BaseTimeProvider
    {
        private DateTime _dateTime;

        public TimeProvider()
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
