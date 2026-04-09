Calculator UPM Package — Usage Guide

Overview

This package provides a small, generic calculator library designed to be consumed as a Unity UPM package or from plain .NET projects. It exposes a simple `Calculator` API with basic binary operations (Add, Subtract, Multiply, Divide) and unary operations (Square, Root). The package focuses on safe execution (returns OperationResult objects with success/failure details) and is generic over numeric types.

Namespaces

- Ssstudio.Calculator — main API (Calculator, ICalculator, UnaryOperations, OperationExecutor)
- Ssstudio.Calculator.Models — operation models (OperationParameters, OperationResult)

Installation (Unity as UPM local package)

1. In Unity Editor: Window → Package Manager.
2. Click the + button → "Add package from disk...".
3. Select the folder that contains this package's `package.json` (the repository root or package folder).

Alternative: Install from Git (if you publish to a Git repo). Use the Git URL with the package name in `package.json`.

Using the API (basic concepts)

- OperationParameters<T>
  - Holds the operands used by operations. For binary ops both `FirstValue` and `SecondValue` are used. For unary ops only `FirstValue` is used.

- OperationResult<T>
  - Returned from any operation. Check `IsSuccess` to detect success. On success `Value` contains the result; on failure `ErrorMessage` explains the problem.

- ICalculator<T>
  - Generic interface for calculator operations. `ICalculator` is an alias for `ICalculator<double>`.

Available operations (public API)

- Add(OperationParameters<T> parameters)
- Subtract(OperationParameters<T> parameters)
- Multiply(OperationParameters<T> parameters)
- Divide(OperationParameters<T> parameters) — checks division by zero and returns a failure on zero divisor.
- Square(OperationParameters<T> parameters) — computes FirstValue * FirstValue.
- Root(OperationParameters<T> parameters) — computes sqrt(FirstValue); returns failure for negative input.

Simple .NET example

```csharp
using Ssstudio.Calculator;
using Ssstudio.Calculator.Models;

var calc = new Calculator(); // Calculator<double>

var p = new OperationParameters(3.0, 4.0);
var sum = calc.Add(p);
if (sum.IsSuccess)
    Console.WriteLine($"Sum = {sum.Value}");
else
    Console.WriteLine($"Add failed: {sum.ErrorMessage}");

var sq = calc.Square(new OperationParameters(5.0, 0.0));
Console.WriteLine(sq.ToDisplayString());

var root = calc.Root(new OperationParameters(9.0, 0.0));
Console.WriteLine(root.ToDisplayString());

var negRoot = calc.Root(new OperationParameters(-4.0, 0.0));
Console.WriteLine(negRoot.ToDisplayString()); // Should show an error
```

Unity example (MonoBehaviour)

Create a script `CalcTester.cs` inside `Assets/Scripts/` in your Unity project:

```csharp
using UnityEngine;
using Ssstudio.Calculator;
using Ssstudio.Calculator.Models;

public class CalcTester : MonoBehaviour
{
    void Start()
    {
        var calc = new Calculator();

        var sq = calc.Square(new OperationParameters(5.0, 0.0));
        Debug.Log("Square(5): " + sq.ToDisplayString());

        var root = calc.Root(new OperationParameters(9.0, 0.0));
        Debug.Log("Root(9): " + root.ToDisplayString());

        var neg = calc.Root(new OperationParameters(-4.0, 0.0));
        Debug.Log("Root(-4): " + neg.ToDisplayString());
    }
}
```

Vector operations

This package also provides simple 2D and 3D vector types with common vector math. The vector types are implemented in the `Ssstudio.Calculator.Vectors` namespace and reuse the `Calculator` for component-wise arithmetic.

Available vector types

- `Vector2D` (`Ssstudio.Calculator.Vectors.Vector2D`)
  - Properties: `double X`, `double Y`
  - Methods: `double Magnitude()`, `double Distance(IVector other)`, `IVector Normalize()`, `double Dot(IVector2 other)`
  - Operators: `+`, `-`, `*` (scalar)

- `Vector3D` (`Ssstudio.Calculator.Vectors.Vector3D`)
  - Properties: `double X`, `double Y`, `double Z`
  - Methods: `double Magnitude()`, `double Distance(IVector other)`, `IVector Normalize()`, `double Dot(IVector3 other)`, `IVector3 Cross(IVector3 other)`
  - Operators: `+`, `-`, `*` (scalar)

Namespaces and interfaces

- `Ssstudio.Calculator.Vectors` contains the vector types and interfaces:
  - `IVector` (base interface), `IVector2`, `IVector3`.

How vector math works

- Component-wise arithmetic (addition, subtraction, scalar multiply) uses the package `Calculator<double>` under the hood. The vector operators create `OperationParameters<double>` for each component, call the corresponding calculator operation, and verify the returned `OperationResult<double>`.
- If an underlying operation fails (for example an unexpected conversion issue), the vector operator methods throw `InvalidOperationException`.
- Methods like `Normalize()` and `Distance()` operate using standard math functions and will throw `InvalidOperationException` for bad inputs (e.g., normalizing a zero vector).

Example: Vector usage (console or Unity script)

```csharp
using Ssstudio.Calculator.Vectors;

var a = new Vector2D(1.0, 2.0);
var b = new Vector2D(3.0, 4.0);

var sum = a + b;                    // Vector2D(4,6)
var diff = a - b;                   // Vector2D(-2,-2)
var scaled = a * 2.0;               // Vector2D(2,4)

double dot = a.Dot(b);             // 1*3 + 2*4 = 11

double mag = a.Magnitude();        // sqrt(1^2 + 2^2)

var unit = (Vector2D)a.Normalize(); // unit vector in a's direction
```

Example: 3D cross product

```csharp
using Ssstudio.Calculator.Vectors;

var u = new Vector3D(1, 0, 0);
var v = new Vector3D(0, 1, 0);
var cross = u.Cross(v); // (0, 0, 1)
```

Error handling notes for vectors

- Vector operator overloads (`+`, `-`, `*`) will throw `InvalidOperationException` if any underlying component operation fails.
- Use `try/catch` around vector operations when working with untrusted inputs or in environments where conversions may fail.
- Methods that rely on geometric constraints (e.g., `Normalize`) throw on invalid conditions (zero-length vector).

Behavior & error handling

- All operations return `IOperationResult<T>` (concrete type `OperationResult<T>`).
- Always check `IsSuccess` before using `Value` to avoid default values from failed operations.
- Operation failures include a helpful error message in `ErrorMessage`.
- `Root` returns failure for negative inputs (the operation throws and the executor converts it into a failure result).
- `Divide` returns failure on division by zero.

Generics and numeric types

- The library is generic (`ICalculator<T>`) and uses `Convert.ToDouble` internally to perform calculations. That means you can call `Calculator<int>` or `Calculator<float>` in addition to `Calculator<double>`, but be aware of conversion and rounding behavior when converting from/to `T`.

How the package is organized (for contributors)

- Runtime/
  - Calculator.cs — public entry point wiring operations
  - OperationExecutor.cs — centralized execution, conversion and error wrapping (DRY)
  - UnaryOperations.cs — math implementations for Square and Root
  - Models/OperationParameters.cs, OperationResult.cs — data models

Extensibility

- To add new operations:
  1. Add the math logic in `UnaryOperations` or a new helper.
  2. Add a public method on `ICalculator<T>` and implement it in `Calculator<T>` by calling `OperationExecutor.ExecuteUnary` or `ExecuteBinary`.

Notes for Unity import (.meta files)

- Unity requires a `.meta` file for each file in the project tree. If Unity reports missing meta files after you add the package, you can:
  - Let Unity re-import the package (it will generate .meta files), or
  - Provide .meta files alongside the files in the package. (If you want, I can generate them for this package.)

Troubleshooting

- If you get runtime issues when testing the `TestRunner` console app, ensure your machine has a compatible .NET runtime for the project's TargetFramework.
- If Unity reports assembly or namespace errors, make sure the package's asmdef and `package.json` are correctly configured and that Unity successfully imported the package.

Contact / Contribution

If you want me to:
- Add unit tests for the new operations,
- Generate Unity .meta files so Unity imports without errors,
- Publish a Git-based UPM distribution (and update package.json accordingly),
- Make a small example Unity project that consumes the package,

...tell me which one and I will implement it.

----

Quick commands (run locally)

Build & run the included TestRunner console app (from Windows PowerShell):

```powershell
cd 'D:\Dot Net project\Calculator_UPM_Package\TestRunner'
dotnet run
```

(If `dotnet run` fails due to runtime mismatch, change `TestRunner/TestRunner.csproj` TargetFramework to a runtime you have installed or install the needed runtime.)

License

Add a license file if you plan to distribute this package.
