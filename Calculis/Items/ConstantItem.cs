namespace Calculis.Core
{
    public sealed class ConstantItem : IValueItem
    {
        public double Value { get; set; }

        public string Name { get; }

        internal ConstantItem(double value)
        {
            Value = value;
        }
    }
}
