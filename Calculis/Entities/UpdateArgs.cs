using System;

namespace Calculis.Core.Entities
{
    public class UpdateArgs : EventArgs
    {
        public DateTime TimeStamp { get; set; }
    }
}
