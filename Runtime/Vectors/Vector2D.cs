// ReSharper disable once CheckNamespace
using System;
using Ssstudio.Calculator.Models;
using Ssstudio.Calculator;

namespace Ssstudio.Calculator.Vectors
{
    /// <summary>
    /// Represents a 2D vector with common vector operations.
    /// Reuses Calculator class for arithmetic operations.
    /// Implements IVector2 interface following LSP (Liskov Substitution Principle).
    /// </summary>
    public class Vector2D : IVector2
    {
        private static readonly ICalculator<double> SCalculator = new Calculator<double>();

        public double X { get; }
        public double Y { get; }

        /// <summary>
        /// Initializes a new instance of Vector2D.
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Calculates the magnitude (length) of the vector.
        /// </summary>
        /// <returns>The magnitude value</returns>
        public double Magnitude()
        {
            return Math.Sqrt((X * X) + (Y * Y));
        }

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The distance value</returns>
        public double Distance(IVector other)
        {
            if (other is not IVector2 vector2D)
                throw new ArgumentException("Can only calculate distance to another 2D vector", nameof(other));

            double dx = X - vector2D.X;
            double dy = Y - vector2D.Y;
            return Math.Sqrt((dx * dx) + (dy * dy));
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

            return new Vector2D(X / magnitude, Y / magnitude);
        }

        /// <summary>
        /// Calculates the dot product with another 2D vector.
        /// </summary>
        /// <param name="other">The other vector</param>
        /// <returns>The dot product value</returns>
        public double Dot(IVector2 other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return (X * other.X) + (Y * other.Y);
        }

        /// <summary>
        /// Vector addition using Calculator class.
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Sum of vectors</returns>
        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException(a == null ? nameof(a) : nameof(b));

            OperationParameters<double> parametersX = new OperationParameters<double>(a.X, b.X);
            IOperationResult<double> resultX = SCalculator.Add(parametersX);
            
            OperationParameters<double> parametersY = new OperationParameters<double>(a.Y, b.Y);
            IOperationResult<double> resultY = SCalculator.Add(parametersY);

            if (!resultX.IsSuccess || !resultY.IsSuccess)
                throw new InvalidOperationException("Addition operation failed");

            double newX = resultX.Value;
            double newY = resultY.Value;
            
            return new Vector2D(newX, newY);
        }

        /// <summary>
        /// Vector subtraction using Calculator class.
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Difference of vectors</returns>
        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException(a == null ? nameof(a) : nameof(b));

            OperationParameters<double> parametersX = new OperationParameters<double>(a.X, b.X);
            IOperationResult<double> resultX = SCalculator.Subtract(parametersX);
            
            OperationParameters<double> parametersY = new OperationParameters<double>(a.Y, b.Y);
            IOperationResult<double> resultY = SCalculator.Subtract(parametersY);

            if (!resultX.IsSuccess || !resultY.IsSuccess)
                throw new InvalidOperationException("Subtraction operation failed");

            double newX = resultX.Value;
            double newY = resultY.Value;

            return new Vector2D(newX, newY);
        }

        /// <summary>
        /// Scalar multiplication using Calculator class.
        /// </summary>
        /// <param name="v">Vector to multiply</param>
        /// <param name="scalar">Scalar value</param>
        /// <returns>Scaled vector</returns>
        public static Vector2D operator *(Vector2D v, double scalar)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            OperationParameters<double> parametersX = new OperationParameters<double>(v.X, scalar);
            IOperationResult<double> resultX = SCalculator.Multiply(parametersX);
            
            OperationParameters<double> parametersY = new OperationParameters<double>(v.Y, scalar);
            IOperationResult<double> resultY = SCalculator.Multiply(parametersY);

            if (!resultX.IsSuccess || !resultY.IsSuccess)
                throw new InvalidOperationException("Multiplication operation failed");

            double newX = resultX.Value;
            double newY = resultY.Value;

            return new Vector2D(newX, newY);
        }

        /// <summary>
        /// Scalar multiplication operator (reversed operands).
        /// </summary>
        /// <param name="scalar">Scalar value</param>
        /// <param name="v">Vector to multiply</param>
        /// <returns>Scaled vector</returns>
        public static Vector2D operator *(double scalar, Vector2D v)
        {
            return v * scalar;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current vector.
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if equal; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            const double epsilon = 1e-9;
            if (!(obj is Vector2D vector)) return false;

            return Math.Abs(X - vector.X) < epsilon && Math.Abs(Y - vector.Y) < epsilon;
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
                return hash;
            }
        }

        /// <summary>
        /// Returns the string representation of the vector.
        /// </summary>
        /// <returns>String in format (X, Y)</returns>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
