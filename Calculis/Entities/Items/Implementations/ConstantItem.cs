using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Items.Implementations
{
    /// <summary>
    /// ConstantItem entity as a CONSTANT contains double Value and string Name.
    /// </summary>
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
