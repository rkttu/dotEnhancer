# dotEnhancer

This is a utility library that collects and provides helper methods using only the APIs supported by each version of the .NET Standard from 1.0 through 2.1.

This library will be developed while maintaining the following characteristics:

- No external dependencies.
- It doesn't add too many APIs that are not supported by .NET Standard.
- Does not add extension methods under the System namespace, but only uses the dotEnhancer namespace (to avoid name confusion)

## Installation

You can install the package via NuGet.

```bash
dotnet add package dotEnhancer
```

## Usage

### Random Number Generator (.NET Standard 1.0 or later)

```csharp
using dotEnhancer;

using var rng = RandomNumberGenerator.Create();
int min = -1000, max = 1000;

for (var i = 0; i < _testCycle; i++)
{
    var result = rng.NextInt32(min, max);
    Console.WriteLine($"Generated Number: {result}");
}
```

## License

This library is under the Apache 2.0 license.

See the [LICENSE](LICENSE) file for details.
