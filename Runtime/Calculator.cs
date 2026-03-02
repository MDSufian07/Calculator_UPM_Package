using System;
using System.Globalization;
using Ssstudio.Calculator.Models;

namespace Ssstudio.Calculator
{
    public class Calculator<T> : ICalculator<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        private IOperationResult<T> ExecuteOperation(OperationParameters<T> parameters, Func<double, double, double> operation,
            string operationName)
        {
            try
            {
                if (parameters == null)
                    return OperationResult<T>.Failure("Parameters cannot be null");

                double a = Convert.ToDouble(parameters.FirstValue, CultureInfo.InvariantCulture);
                double b = Convert.ToDouble(parameters.SecondValue, CultureInfo.InvariantCulture);

                double resultDouble = operation(a, b);

                object converted = Convert.ChangeType(resultDouble, typeof(T), CultureInfo.InvariantCulture);
                return OperationResult<T>.Success((T)converted);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Failure($"{operationName} failed: {ex.Message}");
            }
        }

        public IOperationResult<T> Add(OperationParameters<T> parameters)
        {
            return ExecuteOperation(parameters, (a, b) => a + b, "Addition");
        }

        public IOperationResult<T> Subtract(OperationParameters<T> parameters)
        {
            return ExecuteOperation(parameters, (a, b) => a - b, "Subtraction");
        }

        public IOperationResult<T> Multiply(OperationParameters<T> parameters)
        {
            return ExecuteOperation(parameters, (a, b) => a * b, "Multiplication");
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
                return ExecuteOperation(parameters, (a, b2) => a / b2, "Division");
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Failure($"Division failed: {ex.Message}");
            }
        }
    }

    public class Calculator : Calculator<double>, ICalculator { }
}
