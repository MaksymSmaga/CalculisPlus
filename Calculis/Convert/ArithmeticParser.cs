using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculis.Core.Convert
{
    internal class ArithmeticParser
    {
        private string _expression;
        Regex regExSumBase;
        Regex regExSumBrackets;
        Regex regExSumArguments;

        internal ArithmeticParser(string expression)
        {
            _expression = expression;
            var regExPart = @"((((tag)|__fnc)\d+)|(\d+(\.\d+)?)|([A-Z]+\(.+\)))";
            regExSumBase = new Regex($"{regExPart}(\\+|\\-{regExPart})+");
            regExSumBrackets = new Regex($"\\({regExSumBase}\\)");
            regExSumArguments = new Regex($"(\\(|\\;){regExSumBase}(\\)|\\;)");
        }

        internal string GetFunctionExpression()
        {
            string matchingString = "";
            bool isDetected = true;
            while (isDetected)
            {
                var sumBase = regExSumBase.Match(_expression);
                if (sumBase != null)
                {

                }
            }

            return _expression;
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
