using Calculis.Core;

namespace Calculis.Test.Auxilliary
{
    internal class DataItem : IValueItem
    {
        public string Name { get; }

        public double Value { get; set; }

        public DataItem(string name)
        {
            Name = name;
        }
    }
}
