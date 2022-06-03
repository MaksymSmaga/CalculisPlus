using Calculis.Core.Auxilliary;

namespace Calculis.Core
{
    public sealed class CalculatingItem : IValueItem
    {
        private FunctionBase _function;

        public CalculatingItem(FunctionBase function)
        {
            _function = function;
        }

        public string Name { get; set; }
        public double Value { get { return _function.Function(); } set { } }

        internal void Update(object sender, UpdateArgs args)
        {
            _function.Update(args.Timestamp);
        }
    }
}
