namespace Calculis.Core.Convert
{
    class ItemInfo
    {
        public IValueItem Item { get; set; }
        public string Alias { get; set; }
        public string OriginalExpression { get; set; }
        public string ReplacedExpression { get; set; }
    }
}
