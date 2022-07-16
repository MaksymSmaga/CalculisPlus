using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Items.Implementations
{
    public sealed class ConstantItem : IItem
    {
        public double Value { get; set; }

        public string Name { get; }

        internal ConstantItem(double value)
        {
            Value = value;
        }
    }
}
