using System;

namespace Calculis.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ArgumentsNumberAttribute : Attribute
    {
        public int Number { get; set; }
        public ArgumentsNumberAttribute(int number)
        {
            Number = number;
        }
    }
}
