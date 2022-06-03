﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Calculis.Core.Calculation
{
    internal class FunctionManager
    {
        static IDictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();
        
        static FunctionManager()
        {
            Register("Calculis.Functions.dll");
        }

        internal static void Register(string assemblyName)
        {
            if (assemblyName == null) throw new ArgumentNullException(assemblyName);
            if (_assemblies.ContainsKey(assemblyName)) throw new ArgumentException($"Assembly {assemblyName} has already been registred!");

            _assemblies.Add(assemblyName, Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName)) ??
                        throw new InvalidOperationException($"Assemby {assemblyName} not found! Make shure that it is located in the same directory with the project."));
        }

        internal static FunctionBase Create(string name, IList<IValueItem> args)
        {
            var functionName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();

            Type type = null;
            foreach (var assembly in _assemblies)
                if ((type = assembly.Value.GetType($"{assembly.Key.Replace("dll", "")}{functionName}Function")) != null)
                    break;

            if (type == null)
                throw new ArgumentNullException($"Function \"{name}\" has not detected in plugged assemblies!");

            return (FunctionBase)Activator.CreateInstance(type, args);
        }
    }
}
