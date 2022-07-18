using Calculis.Core.Entities.Items.Abstractions;
using System;

namespace Calculis.Core.Entities.Items.Implementations
{
    public class CashItem : IValue
    {
        public double Value { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
