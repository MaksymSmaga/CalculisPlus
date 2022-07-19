using Calculis.Core.Entities.Items;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculis.Core.Convert
{
    internal sealed class ExpressionParser
    {
        private readonly ICollection<ParametersParser> _parsingParameters;
        private readonly Regex functionExpression;
        private readonly ItemsManager _manager;

        private CultureInfo _culture;

        internal ExpressionParser(ItemsManager manager, CultureInfo culture = null)
        {
            _culture = culture ?? CultureInfo.CurrentCulture;
            _manager = manager;

            var regexExpression = $@"[A-Z]+\(((-)?(((item|__fnc)\d+)|(\d+(\{_culture.NumberFormat.NumberDecimalSeparator}\d+)?))\;*)+\)";
            functionExpression = new Regex(regexExpression);

            var regExPart = $@"((((item)|__fnc)\d+)|(\d+(\{_culture.NumberFormat.NumberDecimalSeparator}\d+)?))";
            var regNegPart = $"((-)?{regExPart})";

            //The order of expression is important: it correspondes to priorities of checking operations
            _parsingParameters = new List<ParametersParser>();
            _parsingParameters.Add(new ParametersParser($"{regExPart}(\\<>{regNegPart})+", "<>", "NEQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("<>", ";"),
                (str) => str.Replace("<>", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}(\\>={regNegPart})+", ">=", "MOREEQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace(">=", ";"),
                (str) => str.Replace(">=", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}(\\>{regNegPart})+", ">", "MORE",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace(">", ";"),
                (str) => str.Replace(">", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}(\\<={regNegPart})+", "<=", "LESSEQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("<=", ";"),
                (str) => str.Replace("<=", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}(\\<{regNegPart})+", "<", "LESS",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("<", ";"),
                (str) => str.Replace("<", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}(\\={regNegPart})+", "=", "EQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("=", ";"),
                (str) => str.Replace("=", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}([\\*\\/]{regNegPart})+", "*", "MUL",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("*", ";").Replace("/", ";/").Replace(";;", ";"),
                (str) => str.Replace("*", ";").Replace("/", ";/").Replace(";;", ";")));
            _parsingParameters.Add(new ParametersParser($"{regExPart}([\\+\\-]{regExPart})+", "+", "SUM",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("+", ";").Replace("-", ";-").Replace(";;", ";"),
                (str) => str.Replace("+", ";").Replace("-", ";-").Replace(";;", ";")));
        }

        internal string ToFunctionView(string expression)
        {
            var newExpression = Parse(expression).Last();

            var hasEntries = true;
            while (hasEntries)
            {
                hasEntries = false;
                foreach(var entry in _manager.ExpressionAlias.Values)
                    if (newExpression.Contains(entry))
                    {
                        hasEntries = true;
                        newExpression = newExpression.Replace(entry, _manager.GetExpression(entry));
                    }
            }

            return newExpression;
        }

        internal ICollection<string> Parse(string expression)
        {
            var expressionsList = new List<string>();
            string functionString;
            string newString;
            string functionAlias;

            expression = expression.Replace(" ", "");

            while (GetNextExpression(expression, out functionString, out newString))
            {
                functionAlias = ExtractAlias(expressionsList, functionString, newString);
                expression = expression.Replace(functionString, functionAlias);
            }

            if(newString != "")
                ExtractAlias(expressionsList, functionString, newString);

            return expressionsList;
        }

        private string ExtractAlias(ICollection<string> expressions, string originalString, string newString)
        {
            var functionAlias = "";
            if (_manager.ExpressionAlias.ContainsKey(newString))
                functionAlias = _manager.ExpressionAlias[newString];
            else
                functionAlias = _manager.AddAlias(newString, originalString);

            expressions.Add(newString);

            return functionAlias;
        }

        private bool GetNextExpression(string expression, out string foundSubstring, out string newSubstring)
        {
            var isFound = false;
            Match match = null;

            foundSubstring = newSubstring = "";

            foreach(var parameter in _parsingParameters)
                if (parameter.Match(expression).Length > 0)
                {
                    foundSubstring = parameter.Substring;
                    newSubstring = parameter.Replacement;
                    
                    isFound = true;
                    break;
                }
            
            if(!isFound)
            {
                match = functionExpression.Match(expression);
                foundSubstring = newSubstring = match?.Value;
            }

            return foundSubstring != "" && foundSubstring != expression;
        }
    }
}
