using System;
using System.Text.RegularExpressions;

namespace Calculis.Core.Convert
{
    internal class ParsingParameters
    {
        private string _regex;
        private Regex Regex { get; set; }
        private string Sign { get; set; }
        private string FunctionName { get; set; }
        private Func<string, string> ConvertAction { get; set; }
        private Func<string, string> ConvertBracketAction { get; set; }

        public string Substring { get; private set; }
        public string Replacement { get; private set; }

        public ParsingParameters(string regex, string sign, string functionName, Func<string, string> bracketAction, Func<string, string> action)
        {
            _regex = regex;
            Regex = new Regex(regex);
            Sign = sign;
            FunctionName = functionName;
            ConvertAction = action;
            ConvertBracketAction = bracketAction;
        }

        public string Match(string expression)
        {
            var result = "";

            Regex = new Regex($"\\({_regex}\\)");
            result = Regex.Match(expression).Value;
            if(result.Length > 0)
            {
                Substring = result;
                Replacement = $"{FunctionName}({ConvertBracketAction(result)})";

                return result;
            }

            Regex = new Regex(_regex);
            result = Regex.Match(expression).Value;
            if (result.Length > 0)
            {
                Substring = result;
                Replacement = $"{FunctionName}({ConvertAction(result)})";

                return result;
            }

            return result;
        }
    }
}
