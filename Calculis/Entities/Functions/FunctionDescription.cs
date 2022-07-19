namespace Calculis.Core.Entities.Functions
{
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
