using System;
using System.Text.RegularExpressions;

namespace Calculis.Core.Convert
{
    internal class ParametersParser
    {
        private string _regex;
        private Regex Regex { get; set; }
        private string MathSymbol { get; set; }
        private string FunctionName { get; set; }


        private Func<string, string> ConvertAction { get; set; }
        private Func<string, string> ConvertparenthesesAction { get; set; }


        public string Substring { get; private set; }
        public string Replacement { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="mathSymbol">Math Symbol to parse: =, <, > ....</param>
        /// <param name="functionName"></param>
        /// <param name="parenthesesAction"></param>
        /// <param name="action"></param>
        public ParametersParser(string regex, string mathSymbol, string functionName, 
                                Func<string, string> parenthesesAction, 
                                Func<string, string> action)
        {
            _regex = regex;
            Regex = new Regex(regex);
            MathSymbol = mathSymbol;
            FunctionName = functionName;
            ConvertAction = action;
            ConvertparenthesesAction = parenthesesAction;
        }

        public string Match(string expression)
        {
            var result = "";

            Regex = new Regex($"\\({_regex}\\)");
            result = Regex.Match(expression).Value;

            if(result.Length > 0)
            {
                Substring = result;
                Replacement = $"{FunctionName}({ConvertparenthesesAction(result)})";

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
