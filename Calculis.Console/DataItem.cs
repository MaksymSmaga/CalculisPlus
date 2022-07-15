using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Console
{
    class DataItem : IValueItem
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public DataItem(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
