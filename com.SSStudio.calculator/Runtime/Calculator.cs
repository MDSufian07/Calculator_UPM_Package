namespace Ssstudio.Calculator;

using Ssstudio.Calculator.Models;

public class Calculator<T> : ICalculator<T> where T : struct, IComparable, IFormattable, IConvertible
{
    private IOperationResult<T> ExecuteOperation(OperationParameters<T> parameters, Func<T, T, T> operation, string operationName)
    {
        try
        {
            if (parameters == null)
                return OperationResult<T>.Failure("Parameters cannot be null");

            T result = operation(parameters.FirstValue, parameters.SecondValue);
            return OperationResult<T>.Success(result);
        }
        catch (Exception ex)
        {
            return OperationResult<T>.Failure($"{operationName} failed: {ex.Message}");
        }
    }

    public IOperationResult<T> Add(OperationParameters<T> parameters)
    {
        return ExecuteOperation(parameters, (a, b) => (dynamic)a + (dynamic)b, "Addition");
    }

    public IOperationResult<T> Subtract(OperationParameters<T> parameters)
    {
        return ExecuteOperation(parameters, (a, b) => (dynamic)a - (dynamic)b, "Subtraction");
    }

    public IOperationResult<T> Multiply(OperationParameters<T> parameters)
    {
        return ExecuteOperation(parameters, (a, b) => (dynamic)a * (dynamic)b, "Multiplication");
    }

    public IOperationResult<T> Divide(OperationParameters<T> parameters)
    {
        try
        {
            if (parameters == null)
                return OperationResult<T>.Failure("Parameters cannot be null");

            if (((dynamic)parameters.SecondValue) == 0)
                return OperationResult<T>.Failure("Division by zero is not allowed");

            return ExecuteOperation(parameters, (a, b) => (dynamic)a / (dynamic)b, "Division");
        }
        catch (Exception ex)
        {
            return OperationResult<T>.Failure($"Division failed: {ex.Message}");
        }
    }
}

public class Calculator : Calculator<double>, ICalculator { }
