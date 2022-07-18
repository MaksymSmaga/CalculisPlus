using System;

namespace Calculis.Core.Entities.TimeProviders
{
    public class UpdateArgs : EventArgs
    {
        public DateTime TimeStamp { get; set; }
    }
}
