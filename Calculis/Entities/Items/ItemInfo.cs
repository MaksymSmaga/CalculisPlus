using Calculis.Core.Entities.Items.Abstractions;

namespace Calculis.Core.Entities.Items
{
    /// <summary>
    /// ItemInfo entity contains Item Entity and info-properties.
    /// </summary>
    class ItemInfo
    {
        public IItem Item { get; set; }
        public string Alias { get; set; }
        public string OriginalExpression { get; set; }
        public string ReplacedExpression { get; set; }
    }
}
