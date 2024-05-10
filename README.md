# dotEnhancer

[![NuGet Version](https://img.shields.io/nuget/v/dotEnhancer)](https://www.nuget.org/packages/dotEnhancer/) [![GitHub Sponsors](https://img.shields.io/github/sponsors/rkttu)](https://github.com/sponsors/rkttu/)

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

### Random Number Generator (.NET Standard 1.3 or later)

```csharp
using dotEnhancer;

using var rng = RandomNumberGenerator.Create();
int min = -1000, max = 1000;

Console.Out.WriteLine($"Single Try: {rng.NextInt64()}");

foreach (var result in rng.EnumerateInt32s(_testCycle, min, max))
{
    // assert
    Console.Out.WriteLine("Number: {result}");
}
```

### Write Encoded String into Stream (.NET Standard 1.0 or later)

```csharp
using dotEnhancer;

var value = "Hello, World!";
var targetEncoding = new UTF8Encoding(false);
using var writableStream = new MemoryStream();
writableStream.WriteStringWithEncoding(value, targetEncoding);
```

### Initiate String Builder (.NET Standard 1.0 or later)

```csharp
using dotEnhancer;

var sb = new char[] { 'H', 'e', 'l', 'l', 'o', }.AsStringBuilder();
sb.AppendLine(", World!");
Console.Out.WriteLine(sb.ToString());
```

### Overwrite Text and Secure Delete (.NET Standard 1.3 or later)

```csharp
var fileInfo = new FileInfo(Path.GetTempFileName());
fileInfo.OverwriteAllText("Hello, World!", new UTF8Encoding(false));
fileInfo.SecureDelete(SecureDeleteObfuscationMode.All);
```

## License

This library is under the Apache 2.0 license.

See the [LICENSE](LICENSE) file for details.
