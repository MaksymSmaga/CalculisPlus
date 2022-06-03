using System;

namespace Calculis.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ArgumentsTypeAttribute : Attribute
    {
        public int ArgNumber { get; set; }
        public Type Type { get; set; }
        public ArgumentsTypeAttribute(int argNumber, Type type)
        {
            ArgNumber = argNumber;
            Type = type;
        }
    }
}
