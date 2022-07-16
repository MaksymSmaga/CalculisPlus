using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Console
{
    class DataItem : IItem
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
