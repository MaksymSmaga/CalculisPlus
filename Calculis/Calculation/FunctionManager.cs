using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Calculis.Core.Calculation
{
    public class FunctionManager
    {
        //TODO: implement factory method for plugged assemblies.
        public static FunctionBase Create(string name, IList<IValueItem> args)
        {
            var assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Calculis.Functions.dll"));
            var functionName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            var type = assembly.GetType($"Calculis.Functions.{functionName}Function");

            if (type == null)
                throw new ArgumentOutOfRangeException();

            return (FunctionBase)Activator.CreateInstance(type, args);
        }
    }
}
