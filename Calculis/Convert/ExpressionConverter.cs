using Calculis.Core.Auxilliary;
using Calculis.Core.Calculation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace Calculis.Core.Convert
{
    internal sealed class ExpressionConverter
    {
        IDictionary<string, IValueItem> _items;
        IEnumerable<string> _nameSchemes;
        Regex functionExpression;

        internal ExpressionConverter(IDictionary<string, IValueItem> items, IEnumerable<string> nameSchemes)
        {
            _items = items;
            _nameSchemes = nameSchemes;

            var regexExpression = $"[A-Z]+\\(((-)?((({string.Join("|", nameSchemes)}|__fnc)\\d+)|(\\d+(\\.\\d+)?))\\;*)+\\)";
            functionExpression = new Regex(regexExpression);
        }

        internal CalculatingItem ParseExpression(string expression)
        {
            return CreateItem(GetFunction(expression));
        }

        private FunctionBase GetFunction(string expression)
        {
            FunctionDescription functionDescription;
            var functionsDict = new Dictionary<string, IValueItem>();
            string functionString;
            int counter = 0;

            expression = expression.Replace(" ", "");

            while (GetNextExpression(expression, out functionString))
            {
                functionDescription = new FunctionDescription(functionString);
                var args = ExtractArgs(functionString, functionsDict);

                var functionAlias = $"__fnc{++counter}";
                expression = expression.Replace(functionString, functionAlias);

                functionsDict.Add(functionAlias, CreateItem(FunctionManager.Create(functionDescription.Name, args)));
            }

            functionDescription = new FunctionDescription(expression);
            return FunctionManager.Create(functionDescription.Name, ExtractArgs(expression, functionsDict));
        }

        private CalculatingItem CreateItem(FunctionBase function)
        {
            var item = new CalculatingItem(function);

            return item;
        }

        private string CastingToFunctionView(string expression)
        {
            return expression;
        }

        private IList<IValueItem> ExtractArgs(string expression, Dictionary<string, IValueItem> functionsDict)
        {
            var functionDescription = new FunctionDescription(expression);
            var args = new List<IValueItem>();
            foreach (var argString in functionDescription.Args)
            {
                if (_items.ContainsKey(argString))
                    args.Add(_items[argString]);
                else if (_items.ContainsKey(argString.Replace("-", "")))
                    args.Add(new CalculatingItem(FunctionManager.Create("MUL", new List<IValueItem> { new ConstantItem(-1), _items[argString.Replace("-", "")] })));
                else if (functionsDict.ContainsKey(argString))
                    args.Add(functionsDict[argString]);
                else if (double.TryParse(argString.Replace('.', ','), out double result))
                    args.Add(new ConstantItem(result));
                else
                    throw new ArgumentOutOfRangeException(argString);
            }

            return args;
        }

        private bool GetNextExpression(string expression, out string substring)
        {
            expression = CastingToFunctionView(expression);

            var match = functionExpression.Match(expression);
            substring = match?.Value;

            return !(match == null || expression == substring);
        }
    }

    struct FunctionDescription
    {
        internal string Name;
        internal string[] Args;

        internal FunctionDescription(string expression)
        {
            var bracketIndex = expression.IndexOf('(');
            Name = expression.Substring(0, bracketIndex);
            Args = expression.Substring(bracketIndex).Trim('(', ')').Split(';');
        }
    }
}
