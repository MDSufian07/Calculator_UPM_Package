using System;

namespace Ssstudio.Calculator
{
    /// <summary>
    /// Contains standalone unary math operations used by the Calculator.
    /// Kept separate so the main `Calculator` class can stay as the public entry point.
    /// </summary>
    public static class UnaryOperations
    {
        public static double Square(double a) => a * a;

        public static double Root(double a)
        {
            if (a < 0)
                throw new InvalidOperationException("Cannot take square root of a negative number");
            return Math.Sqrt(a);
        }
    }
}
