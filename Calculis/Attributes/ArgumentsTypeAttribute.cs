using System;

namespace Calculis.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ArgumentsTypeAttribute : Attribute
    {
        public int ArgNumber { get; }
        public Type Type { get; }
        public double MinValue { get; } = double.MinValue;
        public double MaxValue { get; } = double.MaxValue;

        public ArgumentsTypeAttribute(int argNumber, Type type)
        {
            ArgNumber = argNumber;
            Type = type;
        }

        public ArgumentsTypeAttribute(int argNumber, Type type, double min) : this(argNumber, type)
        {
            if (!Equals(Type, typeof(ConstantItem)))
            {
                throw new ArgumentException("Range can be only assigned for ConstantItem!");
            }

            MinValue = min;
        }

        public ArgumentsTypeAttribute(int argNumber, Type type, double min, double max) : this(argNumber, type, min)
        {
            MaxValue = max;

            if (MinValue > MaxValue)
            {
                throw new ArgumentException("Incorrect range of values!");
            }
        }
    }
}
