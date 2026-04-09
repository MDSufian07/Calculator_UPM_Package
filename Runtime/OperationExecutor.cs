using System;
using System.Globalization;
using Ssstudio.Calculator.Models;

namespace Ssstudio.Calculator
{
    /// <summary>
    /// Centralizes parameter validation, conversion and result mapping for operations.
    /// Reduces duplication between binary and unary executors.
    /// </summary>
    public static class OperationExecutor
    {
        public static IOperationResult<T> ExecuteBinary<T>(OperationParameters<T> parameters, Func<double, double, double> operation, string operationName)
            where T : struct, IComparable, IFormattable, IConvertible
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

        public static IOperationResult<T> ExecuteUnary<T>(OperationParameters<T> parameters, Func<double, double> operation, string operationName)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            try
            {
                if (parameters == null)
                    return OperationResult<T>.Failure("Parameters cannot be null");

                double a = Convert.ToDouble(parameters.FirstValue, CultureInfo.InvariantCulture);

                double resultDouble = operation(a);

                object converted = Convert.ChangeType(resultDouble, typeof(T), CultureInfo.InvariantCulture);
                return OperationResult<T>.Success((T)converted);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Failure($"{operationName} failed: {ex.Message}");
            }
        }
    }
}
