using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;

namespace Calculis.Core.Entities.Functions.Abstractions.Base
{
    public abstract class BaseFunction
    {
        protected IList<IItem> _args;
        public string Name { get; set; }
        public string Description { get; }
        public virtual Func<double> Function { get; protected set; }

        public BaseFunction(IList<IItem> args)
        {
            _args = args;
            Name = GetType().Name.Replace("Function", "").ToUpper();

            ValidateArgs();
        }

        public virtual void ValidateArgs()
        {
            object[] numberAttributes = GetType().GetCustomAttributes(typeof(ArgsNumAttribute), true);
            object[] typeAttributes = GetType().GetCustomAttributes(typeof(ArgsTypeAttribute), false);

            if (numberAttributes.Length > 0)
            {
                var numberAttribute = numberAttributes[0] as ArgsNumAttribute;
                if (numberAttribute.Number > 0 && numberAttribute.Number != _args.Count)
                {
                    throw new ArgumentException("Number of arguments is not correspond to specification!");
                }
                else if (_args.Count < numberAttribute.MinNumber || _args.Count > numberAttribute.MaxNumber)
                {
                    throw new ArgumentException("Number of arguments is out of range!");
                }

                if (numberAttribute.Number > 0 && typeAttributes.Length > numberAttribute.Number ||
                    typeAttributes.Length > numberAttribute.MaxNumber)
                    throw new ArithmeticException("Number of attributes exeeds the specified number of arguments!");
            }

            foreach (var attribute in typeAttributes)
            {
                if ((attribute as ArgsTypeAttribute).ArgNumber >= _args.Count)
                    throw new ArithmeticException("Number of attributes exeeds the number of arguments!");

                if (attribute is ArgsTypeAttribute attr2)
                    if (!Equals(_args[attr2.ArgNumber].GetType(), attr2.Type))
                        throw new ArgumentException("Arguments does not correspond to specified type!");
            }

        }

        public virtual void Update(DateTime dateTime)
        {
            return;
        }
    }
}
