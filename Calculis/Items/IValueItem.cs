using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Core
{
    public interface IValueItem : IValue
    {
        string Name { get; }
    }
}
