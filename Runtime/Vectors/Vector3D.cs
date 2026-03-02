using System;
using Ssstudio.Calculator.Models;
using Ssstudio.Calculator;

namespace Ssstudio.Calculator.Vectors
{
    /// <summary>
    /// Represents a 3D vector with common vector operations.
    /// Reuses Calculator class for arithmetic operations.
    /// Implements IVector3 interface following LSP (Liskov Substitution Principle).
    /// </summary>
    public class Vector3D : IVector3
    {
        private static readonly ICalculator<double> SCalculator = new Calculator<double>();

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        /// <summary>
        /// Initializes a new instance of Vector3D.
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Calculates the magnitude (length) of the vector.
        /// </summary>
        /// <returns>The magnitude value</returns>
        public double Magnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The distance value</returns>
        public double Distance(IVector other)
        {
            if (other is not IVector3 vector3D)
                throw new ArgumentException("Can only calculate distance to another 3D vector", nameof(other));

            double dx = X - vector3D.X;
            double dy = Y - vector3D.Y;
            double dz = Z - vector3D.Z;
            return Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
        }

        /// <summary>
        /// Returns a normalized vector (unit length) in the same direction.
        /// </summary>
        /// <returns>A unit vector in the same direction</returns>
        public IVector Normalize()
        {
            double magnitude = this.Magnitude();
            if (magnitude == 0)
                throw new InvalidOperationException("Cannot normalize a zero vector");

            return new Vector3D(X / magnitude, Y / magnitude, Z / magnitude);
        }

        /// <summary>
        /// Calculates the dot product with another 3D vector.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The dot product value</returns>
        public double Dot(IVector3 other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return (X * other.X) + (Y * other.Y) + (Z * other.Z);
        }

        /// <summary>
        /// Calculates the cross product with another 3D vector.
        /// Returns a new vector perpendicular to both input vectors.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The cross product vector</returns>
        public IVector3 Cross(IVector3 other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double x = (Y * other.Z) - (Z * other.Y);
            double y = (Z * other.X) - (X * other.Z);
            double z = (X * other.Y) - (Y * other.X);

            return new Vector3D(x, y, z);
        }

        /// <summary>
        /// Vector addition using Calculator class.
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Sum of vectors</returns>
        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException(a == null ? nameof(a) : nameof(b));

            OperationParameters<double> parametersX = new OperationParameters<double>(a.X, b.X);
            IOperationResult<double> resultX = SCalculator.Add(parametersX);
            
            OperationParameters<double> parametersY = new OperationParameters<double>(a.Y, b.Y);
            IOperationResult<double> resultY = SCalculator.Add(parametersY);
            
            OperationParameters<double> parametersZ = new OperationParameters<double>(a.Z, b.Z);
            IOperationResult<double> resultZ = SCalculator.Add(parametersZ);

            if (!resultX.IsSuccess || !resultY.IsSuccess || !resultZ.IsSuccess)
                throw new InvalidOperationException("Addition operation failed");

            double newX = resultX.Value;
            double newY = resultY.Value;
            double newZ = resultZ.Value;
            
            return new Vector3D(newX, newY, newZ);
        }

        /// <summary>
        /// Vector subtraction using Calculator class.
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Difference of vectors</returns>
        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException(a == null ? nameof(a) : nameof(b));

            OperationParameters<double> parametersX = new OperationParameters<double>(a.X, b.X);
            IOperationResult<double> resultX = SCalculator.Subtract(parametersX);
            
            OperationParameters<double> parametersY = new OperationParameters<double>(a.Y, b.Y);
            IOperationResult<double> resultY = SCalculator.Subtract(parametersY);
            
            OperationParameters<double> parametersZ = new OperationParameters<double>(a.Z, b.Z);
            IOperationResult<double> resultZ = SCalculator.Subtract(parametersZ);

            if (!resultX.IsSuccess || !resultY.IsSuccess || !resultZ.IsSuccess)
                throw new InvalidOperationException("Subtraction operation failed");

            double newX = resultX.Value;
            double newY = resultY.Value;
            double newZ = resultZ.Value;
            
            return new Vector3D(newX, newY, newZ);
        }

        /// <summary>
        /// Scalar multiplication using Calculator class.
        /// </summary>
        /// <param name="v">Vector to multiply</param>
        /// <param name="scalar">Scalar value</param>
        /// <returns>Scaled vector</returns>
        public static Vector3D operator *(Vector3D v, double scalar)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            OperationParameters<double> parametersX = new OperationParameters<double>(v.X, scalar);
            IOperationResult<double> resultX = SCalculator.Multiply(parametersX);
            
            OperationParameters<double> parametersY = new OperationParameters<double>(v.Y, scalar);
            IOperationResult<double> resultY = SCalculator.Multiply(parametersY);
            
            OperationParameters<double> parametersZ = new OperationParameters<double>(v.Z, scalar);
            IOperationResult<double> resultZ = SCalculator.Multiply(parametersZ);

            if (!resultX.IsSuccess || !resultY.IsSuccess || !resultZ.IsSuccess)
                throw new InvalidOperationException("Multiplication operation failed");

            double newX = resultX.Value;
            double newY = resultY.Value;
            double newZ = resultZ.Value;

            return new Vector3D(newX, newY, newZ);
        }

        /// <summary>
        /// Scalar multiplication operator (reversed operands).
        /// </summary>
        /// <param name="scalar">Scalar value</param>
        /// <param name="v">Vector to multiply</param>
        /// <returns>Scaled vector</returns>
        public static Vector3D operator *(double scalar, Vector3D v)
        {
            return v * scalar;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current vector.
        /// Uses an epsilon comparison for floating point values to avoid precision issues.
        /// </summary>
        public override bool Equals(object obj)
        {
            const double epsilon = 1e-9;
            if (!(obj is Vector3D vector)) return false;

            return Math.Abs(X - vector.X) < epsilon &&
                   Math.Abs(Y - vector.Y) < epsilon &&
                   Math.Abs(Z - vector.Z) < epsilon;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns the string representation of the vector.
        /// </summary>
        /// <returns>String in format (X, Y, Z)</returns>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
