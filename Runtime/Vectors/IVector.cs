namespace Ssstudio.Calculator.Vectors
{
    /// <summary>
    /// Base interface for vector operations implementing DRY principle.
    /// Defines common vector operations for both 2D and 3D vectors.
    /// </summary>
    public interface IVector
    {
        /// <summary>
        /// Calculates the magnitude (length) of the vector.
        /// </summary>
        double Magnitude();

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        double Distance(IVector other);

        /// <summary>
        /// Normalizes the vector to unit length.
        /// </summary>
        IVector Normalize();
    }
}
