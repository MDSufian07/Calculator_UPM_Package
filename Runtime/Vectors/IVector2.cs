namespace Ssstudio.Calculator.Vectors;

/// <summary>
/// Extended interface for 2D vector operations.
/// Inherits from IVector to apply DRY principle.
/// </summary>
public interface IVector2 : IVector
{
    double X { get; }
    double Y { get; }

    double Dot(IVector2 other);
}
