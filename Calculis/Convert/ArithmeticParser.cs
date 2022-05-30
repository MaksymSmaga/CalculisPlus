using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculis.Core.Convert
{
    public class ArithmeticParser
    {
        private IDictionary<string, string> _expressionsReplaces = new Dictionary<string, string>();
        private readonly ICollection<ParsingParameters> _parsingParameters = new List<ParsingParameters>();

        private string _expression;
        Regex regExSumBase;
        Regex regExSumBrackets;

        Regex regExMulBase;
        Regex regExMulBrackets;

        Regex regExMoreBase;
        Regex regExMoreBrackets;

        Regex functionExpression;

        public ArithmeticParser()
        {
            var regexExpression = $"[A-Z]+\\(((-)?(((item|__fnc)\\d+)|(\\d+(\\.\\d+)?))\\;*)+\\)";
            functionExpression = new Regex(regexExpression);

            //var regExPart = @"((((item)|__fnc)\d+)|(\d+(\.\d+)?)|([A-Z]+\(.+\)))";
            var regExPart = @"((((item)|__fnc)\d+)|(\d+(\.\d+)?))";

            /*regExMoreBase = new Regex($"{regExPart}(\\>{regExPart})+");
            regExMoreBrackets = new Regex($"\\({regExMulBase}\\)");

            regExSumBase = new Regex($"{regExPart}([\\+|\\-]{regExPart})+");
            regExSumBrackets = new Regex($"\\({regExSumBase}\\)");

            regExMulBase = new Regex($"{regExPart}([\\*|\\/]{regExPart})+");
            regExMulBrackets = new Regex($"\\({regExMulBase}\\)");*/

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
            var newExpression = Convert(expression);

            var hasEntries = true;
            while (hasEntries)
            {
                hasEntries = false;
                foreach(var entry in _expressionsReplaces.Keys)
                    if (newExpression.Contains(entry))
                    {
                        hasEntries = true;
                        newExpression = newExpression.Replace(entry, _expressionsReplaces[entry]);
                    }
            }

            return newExpression;
        }

        private string Convert(string expression)
        {
            FunctionDescription functionDescription;
            string functionString;
            string newString;
            string functionAlias;
            int counter = 0;

            expression = expression.Replace(" ", "");

            while (GetNextExpression(expression, out functionString, out newString))
            {
                functionAlias = $"__fnc{++counter}";
                _expressionsReplaces.Add(functionAlias, newString);
                expression = expression.Replace(functionString, functionAlias);
            }

            expression = expression.Replace(functionString, newString);

            return expression;
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

        private List<string> GetArguments(string expression)
        {
            var argsList = new List<string>();
            List<int> signIndexes = new List<int>();
            bool cont = true;

            while (cont)
            {
                int plusSignIndex = expression.IndexOf('+');

                if (plusSignIndex > -1)
                    signIndexes.Add(plusSignIndex);
                else
                    cont = false;
            }

            for (int i = 0; i < signIndexes.Count; i++)
            {
                argsList.Add(expression.Substring(signIndexes[i], signIndexes[i + 1]));
                var arr = expression.Split('+', '-');
            }

            return argsList;
        }
    }
}
