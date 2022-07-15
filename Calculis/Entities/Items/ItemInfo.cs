using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Items
{
    class ItemInfo
    {
        public IValueItem Item { get; set; }
        public string Alias { get; set; }
        public string OriginalExpression { get; set; }
        public string ReplacedExpression { get; set; }
    }
}
