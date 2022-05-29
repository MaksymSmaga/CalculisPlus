using System;

namespace Calculis.Core.Auxilliary
{
    public abstract class TimeProvider
    {
        public abstract DateTime Now { get; }

        public abstract void Update();
    }
}
