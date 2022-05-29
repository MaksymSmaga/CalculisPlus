using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Core
{
    public sealed class ConstantItem : IValueItem
    {
        public double Value { get; set; }

        public string Name { get; }

        public ConstantItem(double value)
        {
            Value = value;
        }
    }
}
