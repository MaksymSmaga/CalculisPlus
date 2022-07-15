using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Core.Entities.Items.Abstractions
{
    public interface IValueItem : IValue
    {
        string Name { get; }
    }
}
