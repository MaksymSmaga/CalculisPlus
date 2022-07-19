using Calculis.Core.Entities.Items.Abstractions;
using System;

namespace Calculis.Core.Entities.Items.Implementations
{
    /// <summary>
    /// CashItem entity contains Value and DateTimeOffset (Extended DateTime).
    /// Offset - the offset value of the stored time relative to UTC.
    /// </summary>
    public sealed class CashItem : IValue
    {
        public double Value { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
