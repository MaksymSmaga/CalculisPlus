using System;

namespace Calculis.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ArgumentsNumberAttribute : Attribute
    {
        public int? Number { get; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; } = int.MaxValue;
        public ArgumentsNumberAttribute(int number)
        {
            if(number < 0)
            {
                throw new ArgumentOutOfRangeException("number");
            }

            Number = number;
        }

        public ArgumentsNumberAttribute()
        {
            if(MaxNumber <  MinNumber)
            {
                throw new ArgumentException("MaxNumber of attribute cannot be less than MinAttribute");
            }
        }
    }
}
