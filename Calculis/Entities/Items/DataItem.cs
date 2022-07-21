using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Console
{
    /// <summary>
    /// DataItem entity is inheritanced from IValue.
    /// It contains Name,Value.
    /// </summary>
    public class DataItem : IItem
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
