using System;
using Ssstudio.Calculator;
using Ssstudio.Calculator.Models;

class Program
{
    static void Main()
    {
        var calc = new Calculator();

        // Square test
        var sqParams = new OperationParameters(5.0, 0.0);
        var sqResult = calc.Square(sqParams);
        Console.WriteLine($"Square(5) => Success: {sqResult.IsSuccess}, Value: {sqResult.Value}, Msg: {sqResult.ErrorMessage}");

        // Root test
        var rootParams = new OperationParameters(9.0, 0.0);
        var rootResult = calc.Root(rootParams);
        Console.WriteLine($"Root(9) => Success: {rootResult.IsSuccess}, Value: {rootResult.Value}, Msg: {rootResult.ErrorMessage}");

        // Negative root test
        var negRootParams = new OperationParameters(-4.0, 0.0);
        var negRootResult = calc.Root(negRootParams);
        Console.WriteLine($"Root(-4) => Success: {negRootResult.IsSuccess}, Value: {negRootResult.Value}, Msg: {negRootResult.ErrorMessage}");
    }
}
