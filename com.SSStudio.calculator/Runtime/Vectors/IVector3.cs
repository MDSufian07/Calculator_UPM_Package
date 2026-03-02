namespace Ssstudio.Calculator.Vectors;

/// <summary>
/// Extended interface for 3D vector operations.
/// Inherits from IVector to apply DRY principle.
/// </summary>
public interface IVector3 : IVector
{
    double X { get; }
    double Y { get; }
    double Z { get; }

    double Dot(IVector3 other);
    
    /// <summary>
    /// Calculates the cross product with another 3D vector.
    /// Returns a new vector perpendicular to both vectors.
    /// </summary>
    IVector3 Cross(IVector3 other);
}
