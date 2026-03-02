namespace Ssstudio.Calculator.Models;

/// <summary>
/// Generic operation parameters supporting any numeric type.
/// </summary>
public class OperationParameters<T> where T : struct, IComparable, IFormattable, IConvertible
{
    public T FirstValue { get; }
    public T SecondValue { get; }

    public OperationParameters(T firstValue, T secondValue)
    {
        FirstValue = firstValue;
        SecondValue = secondValue;
    }

    public override string ToString() => $"({FirstValue}, {SecondValue})";
}

/// <summary>
/// Non-generic version for backward compatibility.
/// </summary>
public class OperationParameters : OperationParameters<double>
{
    public OperationParameters(double firstValue, double secondValue) 
        : base(firstValue, secondValue)
    {
    }
}
