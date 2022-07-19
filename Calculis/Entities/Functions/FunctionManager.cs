using Calculis.Core.Entities.Functions.Abstractions.Base;
using Calculis.Core.Entities.Items.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Calculis.Core.Entities.Functions
{
    internal class FunctionManager
    {
        static readonly IDictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();
        internal static IDictionary<string, Type> Functions { get; private set; } = new Dictionary<string, Type>();

        static FunctionManager()
        {
            Register("Calculis.Core.dll");
        }

        internal static void Register(string assemblyName)
        {
            //nspace = nspace ?? assemblyName.Replace(".dll", "");

            if (assemblyName == null) throw new ArgumentNullException(assemblyName);
            if (_assemblies.ContainsKey(assemblyName)) throw new ArgumentException($"Assembly {assemblyName} has already been registred!");


            _assemblies.Add(assemblyName, Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName)) ??
                        throw new InvalidOperationException($"Assemby {assemblyName} not found! Make shure that it is located in the same directory with the project."));

            var assemblyTypes = _assemblies[assemblyName].GetTypes();
            foreach (var type in assemblyTypes)
                if (Functions.ContainsKey(type.Name.Replace("Function", "")))
                    throw new InvalidOperationException($"Names conflict in function {type.Name}");

            foreach (var type in assemblyTypes)
                if (type.Name.Contains("Function"))
                    Functions.Add(type.Name.Replace("Function", "").ToUpper(), type);
        }

        internal static BaseFunction Create(string name, IList<IItem> args)
        {
            /*var functionName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();

            Type type = null;
            foreach (var assembly in _assemblies)
                if ((type = assembly.Value.GetType($"{assembly.Key.Replace("dll", "")}{functionName}Function")) != null)
                    break;*/

            if (!Functions.ContainsKey(name)) throw new ArgumentNullException($"Function \"{name}\" has not detected in plugged assemblies!");

            return (BaseFunction)Activator.CreateInstance(Functions[name], args);
        }
    }
}
