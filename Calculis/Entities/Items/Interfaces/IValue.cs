namespace Calculis.Core.Entities.Items.Abstractions
{
    /// <summary>
    /// Every inheritanced entity should have double Value property.
    /// </summary>
    public interface IValue
    {
        double Value { get; set; }
    }
}
