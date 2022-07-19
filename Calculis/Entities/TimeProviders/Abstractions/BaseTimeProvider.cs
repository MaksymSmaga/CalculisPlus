using System;

namespace Calculis.Core.Entities.TimeProviders.Abstractions
{
    public abstract class BaseTimeProvider
    {
        public abstract DateTime Now { get; }

        public abstract void Update();
    }
}
