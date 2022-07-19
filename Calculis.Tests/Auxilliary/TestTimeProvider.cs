using Calculis.Core.Entities.TimeProviders.Abstractions;
using System;

namespace Calculis.Test.Auxilliary
{
    internal class TestTimeProvider : BaseTimeProvider
    {
        private DateTime _dateTime;
        private int step;

        public TestTimeProvider(DateTime start)
        {
            _dateTime = start;
        }

        public void AddSecond(int second = 1)
        {
            step += second;
            _dateTime = _dateTime.AddSeconds(second);
        }

        public override void Update()
        {
            step++;
            _dateTime = _dateTime.AddSeconds(1);
        }

        public override DateTime Now
        {
            get { return _dateTime; }
        }
    }
}
