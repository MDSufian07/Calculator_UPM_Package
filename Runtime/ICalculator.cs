namespace Ssstudio.Calculator
{
    using Ssstudio.Calculator.Models;

    public interface ICalculator<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        IOperationResult<T> Add(OperationParameters<T> parameters);
        IOperationResult<T> Subtract(OperationParameters<T> parameters);
        IOperationResult<T> Multiply(OperationParameters<T> parameters);
        IOperationResult<T> Divide(OperationParameters<T> parameters);
    }

    public interface ICalculator : ICalculator<double> { }
}
