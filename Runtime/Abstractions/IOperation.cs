namespace Ssstudio.Calculator.Abstractions
{

    /// <summary>
    /// Strategy pattern for different operation types.
    /// Follows Open/Closed Principle - open for extension, closed for modification.
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Gets the operation identifier.
        /// </summary>
        char Identifier { get; }

        /// <summary>
        /// Gets the operation description.
        /// </summary>
        string Description { get; }
    }
}