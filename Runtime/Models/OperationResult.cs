// ReSharper disable once CheckNamespace
using System;

namespace Ssstudio.Calculator.Models
{
    public interface IOperationResult<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        bool IsSuccess { get; }
        T Value { get; }
        string ErrorMessage { get; }
        string ToDisplayString();
    }

    public class OperationResult<T> : IOperationResult<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        public bool IsSuccess { get; internal set; }
        public T Value { get; internal set; }
        public string ErrorMessage { get; internal set; } = string.Empty;

        internal OperationResult() { }

        public static IOperationResult<T> Success(T value)
        {
            return new OperationResult<T> { IsSuccess = true, Value = value, ErrorMessage = string.Empty };
        }

        public static IOperationResult<T> Failure(string errorMessage)
        {
            return new OperationResult<T> { IsSuccess = false, Value = default, ErrorMessage = errorMessage };
        }

        public string ToDisplayString() => IsSuccess ? $"Result: {Value}" : $"Error: {ErrorMessage}";
        public override string ToString() => ToDisplayString();
    }

    public interface IOperationResult : IOperationResult<double> { }

    public class OperationResult : OperationResult<double>, IOperationResult
    {
        internal OperationResult() { }
    }
}
