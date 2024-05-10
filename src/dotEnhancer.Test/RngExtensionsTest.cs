using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace dotEnhancer.Test;

public class RngExtensionsTest
{
    public RngExtensionsTest()
    {
        using var rng = RandomNumberGenerator.Create();
        _testCycle = rng.NextInt32(65536, 1024 * 1024);
    }

    private readonly int _testCycle = 1024 * 1024;

    [Fact]
    public void NextBooleanTest()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        bool min = false, max = true;

        // act
        foreach (var result in rng.EnumerateBooleans(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextByteTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        byte min = 100, max = 250;

        // act
        foreach (var result in rng.EnumerateBytes(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextByteTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        byte min = byte.MinValue, max = byte.MaxValue;

        // act
        foreach (var result in rng.EnumerateBytes(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextSByteTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        sbyte min = -80, max = 120;

        // act
        foreach (var result in rng.EnumerateSBytes(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextSByteTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        sbyte min = sbyte.MinValue, max = sbyte.MaxValue;

        // act
        foreach (var result in rng.EnumerateSBytes(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextCharTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        char min = 'A', max = 'z';

        // act
        foreach (var result in rng.EnumerateChars(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextCharTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        char min = char.MinValue, max = char.MaxValue;

        // act
        foreach (var result in rng.EnumerateChars(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt16Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        short min = -1000, max = 1000;

        // act
        foreach (var result in rng.EnumerateInt16s(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt16Test_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        short min = short.MinValue, max = short.MaxValue;

        // act
        foreach (var result in rng.EnumerateInt16s(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt16Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        ushort min = 1000, max = 2000;

        // act
        foreach (var result in rng.EnumerateUInt16s(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt16Test_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        ushort min = ushort.MinValue, max = ushort.MaxValue;

        // act
        foreach (var result in rng.EnumerateUInt16s(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt32Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        int min = -1000, max = 1000;

        // act
        foreach (var result in rng.EnumerateInt32s(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt32Test_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        int min = int.MinValue, max = int.MaxValue;

        // act
        foreach (var result in rng.EnumerateInt32s(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt32Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        uint min = 1000u, max = 2000u;

        // act
        foreach (var result in rng.EnumerateUInt32s(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt32Test_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        uint min = uint.MinValue, max = uint.MaxValue;

        // act
        foreach (var result in rng.EnumerateUInt32s(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt64Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        long min = -1000L, max = 1000L;

        // act
        foreach (var result in rng.EnumerateInt64s(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt64Test_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        long min = long.MinValue, max = long.MaxValue;

        // act
        foreach (var result in rng.EnumerateInt64s(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt64Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        ulong min = 1000uL, max = 2000uL;

        // act
        foreach (var result in rng.EnumerateUInt64s(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt64Test_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        ulong min = ulong.MinValue, max = ulong.MaxValue;

        // act
        foreach (var result in rng.EnumerateUInt64s(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextSingleTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        float min = -1000f, max = 1000f;

        // act
        foreach (var result in rng.EnumerateSingles(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextSingleTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        float min = float.MinValue, max = float.MaxValue;

        // act
        foreach (var result in rng.EnumerateSingles(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDoubleTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        double min = -1000d, max = 1000d;

        // act
        foreach (var result in rng.EnumerateDoubles(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDoubleTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        double min = double.MinValue, max = double.MaxValue;

        // act
        foreach (var result in rng.EnumerateDoubles(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDecimalTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        decimal min = -1000m, max = 1000m;

        // act
        foreach (var result in rng.EnumerateDecimals(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDecimalTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        decimal min = decimal.MinValue, max = decimal.MaxValue;

        // act
        foreach (var result in rng.EnumerateDecimals(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDateTimeTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = DateTime.Now.AddYears(-10);
        var max = DateTime.Now.AddYears(10);

        // act
        foreach (var result in rng.EnumerateDateTimes(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDateTimeTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = DateTime.MinValue;
        var max = DateTime.MaxValue;

        // act
        foreach (var result in rng.EnumerateDateTimes(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDateTimeOffsetTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = DateTimeOffset.Now.AddYears(-10);
        var max = DateTimeOffset.Now.AddYears(10);

        // act
        foreach (var result in rng.EnumerateDateTimeOffsets(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDateTimeOffsetTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = DateTimeOffset.MinValue;
        var max = DateTimeOffset.MaxValue;

        // act
        foreach (var result in rng.EnumerateDateTimeOffsets(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextTimeSpanTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = TimeSpan.FromDays(-10);
        var max = TimeSpan.FromDays(10);

        // act
        foreach (var result in rng.EnumerateTimeSpans(_testCycle, min, max))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextTimeSpanTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = TimeSpan.MinValue;
        var max = TimeSpan.MaxValue;

        // act
        foreach (var result in rng.EnumerateTimeSpans(_testCycle))
        {
            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextGuidTest()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumerateGuids(_testCycle))
        {
            // assert
            Assert.NotEqual(Guid.Empty, result);
        }
    }

    [Fact]
    public void NextVersionTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = new Version(1, 1, 1, 1);
        var max = new Version(10, 10, 10, 10);

        // act
        foreach (var result in rng.EnumerateVersions(_testCycle, min, max))
        {
            // assert
            Assert.NotNull(result);
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextVersionTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumerateVersions(_testCycle))
        {
            // assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Major);
            Assert.NotEqual(0, result.Minor);
            Assert.NotEqual(0, result.Build);
            Assert.NotEqual(0, result.Revision);
        }
    }

    [Fact]
    public void NextIPAddressTest_IPv4()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumerateIPv4Addresses(_testCycle))
        {
            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetwork, result.AddressFamily);
        }
    }

    [Fact]
    public void NextIPAddressTest_IPv6()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumerateIPv6Addresses(_testCycle))
        {
            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetworkV6, result.AddressFamily);
            Assert.Equal(default, result.ScopeId);
        }
    }

    [Fact]
    public void NextIPAddressTest_IPv6_WithScopeId()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var scopeId = rng.NextInt64(min: 0L, max: 4294967295L);

        // act
        foreach (var result in rng.EnumerateIPv6Addresses(_testCycle, scopeId))
        {
            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetworkV6, result.AddressFamily);
            Assert.Equal(scopeId, result.ScopeId);
        }
    }

    [Fact]
    public void NextIPEndPointTest_IPv4()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumerateIPv4EndPoints(_testCycle))
        {
            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetwork, result.AddressFamily);
            Assert.Equal(AddressFamily.InterNetwork, result.Address.AddressFamily);
        }
    }

    [Fact]
    public void NextIPEndPointTest_IPv6()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumerateIPv6EndPoints(_testCycle))
        {
            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetworkV6, result.AddressFamily);
            Assert.Equal(AddressFamily.InterNetworkV6, result.Address.AddressFamily);
            Assert.Equal(default, result.Address.ScopeId);
        }
    }

    [Fact]
    public void NextIPEndPointTest_IPv6_WithScopeId()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var scopeId = rng.NextInt64(min: 0L, max: 4294967295L);

        // act
        foreach (var result in rng.EnumerateIPv6EndPoints(_testCycle, scopeId))
        {
            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetworkV6, result.AddressFamily);
            Assert.Equal(AddressFamily.InterNetworkV6, result.Address.AddressFamily);
            Assert.Equal(scopeId, result.Address.ScopeId);
        }
    }

    [Fact]
    public void NextPortNumberTest()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        // act
        foreach (var result in rng.EnumeratePortNumbers(_testCycle))
        {
            // assert
            Assert.InRange(result, IPEndPoint.MinPort, IPEndPoint.MaxPort);
        }
    }

    [Fact]
    public void NextBase64Test()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        var min = 11;
        var max = 22;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextBase64(min, max);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
