using System;

namespace Calculis.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ArgsNumAttribute : Attribute
    {
        public int? Number { get; }
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; } = int.MaxValue;

        public ArgsNumAttribute(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException("Number of attribute should be greater then Zero");
            }
 
          Number = number;  
        }

        public ArgsNumAttribute()
        {
            if (MaxNumber < MinNumber)
                throw new ArgumentException("MaxNumber of attribute cannot be less than MinAttribute");
        }
    }
}
