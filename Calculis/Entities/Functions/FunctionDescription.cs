namespace Calculis.Core.Entities.Functions
{
    struct FunctionDescription
    {
        internal string Name;
        internal string[] Args;

        internal FunctionDescription(string expression)
        {
            // To determine the first occurrence of the char '(' in a expression.
            var parenthesesIndex = expression.IndexOf('(');

            // To cut substring to the char '('.
            Name = expression.Substring(0, parenthesesIndex);

            // Substring - To cut substring to the char of parenthesesIndex.
            // Trim - To remove white spaces/specified character.
            // Split - To break a delimited string into substrings.
            Args = expression.Substring(parenthesesIndex).Trim('(', ')').Split(';');
        }
    }
}
