namespace Ssstudio.Calculator.Abstractions
{

    /// <summary>
    /// Defines a calculator operation handler.
    /// Follows Strategy Pattern and Single Responsibility Principle.
    /// </summary>
    public interface ICalculatorOperationHandler
    {
        /// <summary>
        /// Executes a calculator operation interactively.
        /// </summary>
        void Execute();
    }
}