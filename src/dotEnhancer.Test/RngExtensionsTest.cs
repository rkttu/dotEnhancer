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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextBoolean();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextByte(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextByte();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextSByte(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextSByte();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextChar(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextChar();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextInt16(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextInt16();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextUInt16(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextUInt16();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextInt32(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextInt32();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt32Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        uint min = 1000, max = 2000;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextUInt32(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextUInt32();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextInt64Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        long min = -1000, max = 1000;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextInt64(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextInt64();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextUInt64Test_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        ulong min = 1000, max = 2000;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextUInt64(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextUInt64();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextSingleTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        float min = -1000, max = 1000;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextSingle(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextSingle();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDoubleTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        double min = -1000, max = 1000;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDouble(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDouble();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextDecimalTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();
        decimal min = -1000, max = 1000;

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDecimal(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDecimal();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDateTime(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDateTime();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDateTimeOffset(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextDateTimeOffset();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextTimeSpan(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextTimeSpan();

            // assert
            Assert.InRange(result, min, max);
        }
    }

    [Fact]
    public void NextGuidTest()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextGuid();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextVersion(min, max);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextVersion();

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextIPAddress(ipv6: false);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextIPAddress(ipv6: true);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var scopeId = rng.NextInt64(min: 0L, max: 4294967295L);
            var result = rng.NextIPAddress(ipv6: true, ipv6ScopeId: scopeId);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextIPEndPoint(ipv6: false);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextIPEndPoint(ipv6: true);

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

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var scopeId = rng.NextInt64(min: 0L, max: 4294967295L);
            var result = rng.NextIPEndPoint(ipv6: true, ipv6ScopeId: scopeId);

            // assert
            Assert.NotNull(result);
            Assert.Equal(AddressFamily.InterNetworkV6, result.AddressFamily);
            Assert.Equal(AddressFamily.InterNetworkV6, result.Address.AddressFamily);
            Assert.Equal(scopeId, result.Address.ScopeId);
        }
    }

    [Fact]
    public void NextPortNumberTest_WithThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextPortNumber();

            // assert
            Assert.InRange(result, IPEndPoint.MinPort, IPEndPoint.MaxPort);
        }
    }

    [Fact]
    public void NextPortNumberTest_WithoutThreshold()
    {
        // arrange
        using var rng = RandomNumberGenerator.Create();

        for (var i = 0; i < _testCycle; i++)
        {
            // act
            var result = rng.NextPortNumber();

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
