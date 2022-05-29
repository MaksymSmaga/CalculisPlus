using System;
using System.Collections.Generic;
using System.Text;

namespace Calculis.Core
{
    interface ICalculis
    {
        void AddItem(IValueItem item);
        CalculatingItem Add(string name, string expression);
        void Update();
    }
}
