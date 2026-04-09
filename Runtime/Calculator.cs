using System;
using System.Globalization;
using Ssstudio.Calculator.Models;

namespace Ssstudio.Calculator
{
    public class Calculator<T> : ICalculator<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        public IOperationResult<T> Add(OperationParameters<T> parameters)
        {
            return OperationExecutor.ExecuteBinary(parameters, (a, b) => a + b, "Addition");
        }

        public IOperationResult<T> Subtract(OperationParameters<T> parameters)
        {
            return OperationExecutor.ExecuteBinary(parameters, (a, b) => a - b, "Subtraction");
        }

        public IOperationResult<T> Multiply(OperationParameters<T> parameters)
        {
            return OperationExecutor.ExecuteBinary(parameters, (a, b) => a * b, "Multiplication");
        }

        public IOperationResult<T> Divide(OperationParameters<T> parameters)
        {
            try
            {
                if (parameters == null)
                    return OperationResult<T>.Failure("Parameters cannot be null");
                double b = Convert.ToDouble(parameters.SecondValue, CultureInfo.InvariantCulture);
                if (b == 0.0)
                    return OperationResult<T>.Failure("Division by zero is not allowed");
                return OperationExecutor.ExecuteBinary(parameters, (a, b2) => a / b2, "Division");
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Failure($"Division failed: {ex.Message}");
            }
        }

        public IOperationResult<T> Square(OperationParameters<T> parameters)
        {
            // Square uses only the FirstValue and delegates to UnaryOperations.Square
            return OperationExecutor.ExecuteUnary(parameters, UnaryOperations.Square, "Square");
        }

        public IOperationResult<T> Root(OperationParameters<T> parameters)
        {
            // Root uses only the FirstValue and delegates to UnaryOperations.Root
            return OperationExecutor.ExecuteUnary(parameters, UnaryOperations.Root, "Square Root");
        }
    }

    public class Calculator : Calculator<double>, ICalculator { }
}
