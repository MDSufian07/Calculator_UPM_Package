namespace Ssstudio.Calculator.Abstractions
{
    
    /// <summary>
    /// Defines handlers for vector operations.
    /// Follows Strategy Pattern and Single Responsibility Principle.
    /// </summary>
    public interface IVectorOperationHandler
    {
        /// <summary>
        /// Executes a vector operation interactively.
        /// </summary>
        void Execute();
    }
}
