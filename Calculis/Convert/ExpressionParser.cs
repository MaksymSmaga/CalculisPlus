using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculis.Core.Convert
{
    public sealed class ExpressionParser
    {
        private readonly ICollection<ParsingParameters> _parsingParameters = new List<ParsingParameters>();

        Regex functionExpression;
        ItemsManager _manager;

        public ExpressionParser(ItemsManager manager)
        {
            _manager = manager;

            var regexExpression = $"[A-Z]+\\(((-)?(((item|__fnc)\\d+)|(\\d+(\\.\\d+)?))\\;*)+\\)";
            functionExpression = new Regex(regexExpression);

            //var regExPart = @"((((item)|__fnc)\d+)|(\d+(\.\d+)?)|([A-Z]+\(.+\)))";
            var regExPart = @"((((item)|__fnc)\d+)|(\d+(\.\d+)?))";

            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\<>{regExPart})+", "<>", "NEQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("<>", ";"),
                (str) => str.Replace("<>", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\>={regExPart})+", ">=", "MOREEQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace(">=", ";"),
                (str) => str.Replace(">=", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\>{regExPart})+", ">", "MORE",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace(">", ";"),
                (str) => str.Replace(">", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\<={regExPart})+", "<=", "LESSEQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("<=", ";"),
                (str) => str.Replace("<=", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\<{regExPart})+", "<", "LESS",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("<", ";"),
                (str) => str.Replace("<", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\={regExPart})+", "=", "EQ",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("=", ";"),
                (str) => str.Replace("=", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}(\\*{regExPart})+", "*", "MUL",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("*", ";"),
                (str) => str.Replace("*", ";")));
            _parsingParameters.Add(new ParsingParameters($"{regExPart}([\\+|\\-]{regExPart})+", "+", "SUM",
                (str) => str.Remove(str.Length - 1, 1).TrimStart('(').Replace("+", ";").Replace("-", ";-").Replace(";;", ";"),
                (str) => str.Replace("+", ";").Replace("-", ";-").Replace(";;", ";")));
        }


        public string ToFunctionView(string expression)
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

        public ICollection<string> Parse(string expression)
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

            return !(foundSubstring == expression);
        }
    }
}
