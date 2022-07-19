namespace Calculis.Core.Entities.Items.Abstractions
{
    /// <summary>
    /// Every inheritanced entity should have string Name property.
    /// IItem has hidden inheritanced double Value property from IValue.
    /// </summary>
    public interface IItem : IValue
    {
        string Name { get; }
    }
}
