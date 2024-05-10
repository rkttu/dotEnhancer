using System;

namespace dotEnhancer
{
#if NETSTANDARD1_3_OR_GREATER
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Cryptography;

    partial class RngExtensions
    {
        /// <summary>
        /// Generates a random boolean value.
        /// </summary>
        /// <typeparam name="TRng">The type of the random number generator.</typeparam>
        /// <param name="rng">The random number generator.</param>
        /// <returns>A random boolean value.</returns>
        public static bool NextBoolean<TRng>(this TRng rng)
            where TRng : RandomNumberGenerator
        {
            var blob = new byte[1];
            rng.GetBytes(blob);
            return (blob[0] & 0x01) == 0; // 첫 번째 비트만 확인
        }

        public static IEnumerable<bool> EnumerateBooleans<TRng>(this TRng rng, int? count = default)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextBoolean();
        }

        public static byte NextByte<TRng>(this TRng rng, byte min = byte.MinValue, byte max = byte.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            // Range의 크기 계산
            var range = max - min;

            // 원하는 범위를 얻기 위해 필요한 범위를 고려한 바이트값 추출
            var blob = new byte[1];
            var scale = (byte)(256 / range);

            while (true)
            {
                rng.GetBytes(blob);
                var result = (byte)(blob[0] / scale); // 0과 (range - 1) 사이의 값을 반환
                if (result < range)
                    return (byte)(result + min); // 원하는 범위로 조정하여 반환
            }
        }

        public static IEnumerable<byte> EnumerateBytes<TRng>(this TRng rng, int? count = default, byte min = byte.MinValue, byte max = byte.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextByte(min, max);
        }

        public static sbyte NextSByte<TRng>(this TRng rng, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            // 범위 계산
            var range = max - min;

            // 원하는 범위를 얻기 위해 필요한 범위를 고려한 바이트값 추출
            var blob = new byte[1];
            var scale = (byte)(256 / range);

            while (true)
            {
                rng.GetBytes(blob);
                var result = blob[0] / scale; // 0과 (range - 1) 사이의 값을 반환
                if (result < range)
                    return (sbyte)(result + min); // 원하는 범위로 조정하여 반환
            }
        }

        public static IEnumerable<sbyte> EnumerateSBytes<TRng>(this TRng rng, int? count = default, sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextSByte(min, max);
        }

        public static char NextChar<TRng>(this TRng rng, char min = char.MinValue, char max = char.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var range = (uint)max - min;
            var threshold = (uint)((1UL << 16) / range) * range;

            while (true)
            {
                var blob = new byte[2];
                rng.GetBytes(blob);
                var value = BitConverter.ToUInt16(blob, 0);

                if (value < threshold)
                    return (char)(value % range + min);
            }
        }

        public static IEnumerable<char> EnumerateChars<TRng>(this TRng rng, int? count = default, char min = char.MinValue, char max = char.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextChar(min, max);
        }

        public static short NextInt16<TRng>(this TRng rng, short min = short.MinValue, short max = short.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            // 범위의 크기를 계산
            int range = max - min + 1;  // 포함되는 모든 값의 개수

            // 원하는 범위를 얻기 위해 필요한 바이트값 추출
            var blob = new byte[2];

            while (true)
            {
                rng.GetBytes(blob);
                int result = BitConverter.ToUInt16(blob, 0);  // 부호 없는 16비트 정수로 변환
                int value = result % range;  // 0과 (range - 1) 사이의 값을 반환
                if (value < range)
                    return (short)(value + min);  // 원하는 범위로 조정하여 반환
            }
        }

        public static IEnumerable<short> EnumerateInt16s<TRng>(this TRng rng, int? count = default, short min = short.MinValue, short max = short.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextInt16(min, max);
        }

        public static ushort NextUInt16<TRng>(this TRng rng, ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var range = (uint)max - min;
            var threshold = (uint)((1UL << 16) / range) * range;

            while (true)
            {
                var blob = new byte[2];
                rng.GetBytes(blob);
                var value = BitConverter.ToUInt16(blob, 0);

                if (value < threshold)
                    return (ushort)(value % range + min);
            }
        }

        public static IEnumerable<ushort> EnumerateUInt16s<TRng>(this TRng rng, int? count = default, ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextUInt16(min, max);
        }

        public static int NextInt32<TRng>(this TRng rng, int min = int.MinValue, int max = int.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var range = (uint)(max - min);
            var threshold = (uint.MaxValue / range) * range;

            while (true)
            {
                var blob = new byte[8];
                rng.GetBytes(blob);
                var value = BitConverter.ToUInt32(blob, 0);

                if (value < threshold)
                    return (int)(value % range + (uint)min);
            }
        }

        public static IEnumerable<int> EnumerateInt32s<TRng>(this TRng rng, int? count = default, int min = int.MinValue, int max = int.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextInt32(min, max);
        }

        public static uint NextUInt32<TRng>(this TRng rng, uint min = uint.MinValue, uint max = uint.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var range = (ulong)(max - min);
            var threshold = ((1UL << 32) / range) * range;

            while (true)
            {
                var blob = new byte[4];
                rng.GetBytes(blob);
                var value = BitConverter.ToUInt32(blob, 0);

                if (value < threshold)
                    return (uint)(value % range + min);
            }
        }

        public static IEnumerable<uint> EnumerateUInt32s<TRng>(this TRng rng, int? count = default, uint min = uint.MinValue, uint max = uint.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextUInt32(min, max);
        }

        public static long NextInt64<TRng>(this TRng rng, long min = long.MinValue, long max = long.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var range = (ulong)(max - min);
            var threshold = (ulong.MaxValue / range) * range;

            while (true)
            {
                var blob = new byte[8];
                rng.GetBytes(blob);
                var value = BitConverter.ToUInt64(blob, 0);

                if (value < threshold)
                    return (long)(value % range + (ulong)min);
            }
        }

        public static IEnumerable<long> EnumerateInt64s<TRng>(this TRng rng, int? count = default, long min = long.MinValue, long max = long.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextInt64(min, max);
        }

        public static ulong NextUInt64<TRng>(this TRng rng, ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var range = max - min;
            var threshold = (ulong.MaxValue / range) * range;

            while (true)
            {
                var blob = new byte[8];
                rng.GetBytes(blob);
                var value = BitConverter.ToUInt64(blob, 0);

                if (value < threshold)
                    return value % range + min;
            }
        }

        public static IEnumerable<ulong> EnumerateUInt64s<TRng>(this TRng rng, int? count = default, ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextUInt64(min, max);
        }

        public static float NextSingle<TRng>(this TRng rng, float min = float.MinValue, float max = float.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            // 안전한 범위로 한정
            if (min < float.MinValue / 2)
                min = float.MinValue / 2;

            if (max > float.MaxValue / 2)
                max = float.MaxValue / 2;

            // 4 바이트 랜덤 바이트 배열 생성
            var blob = new byte[4];
            rng.GetBytes(blob);

            // uint로 변환하여 0과 1 사이의 부동 소수점 수로 스케일링
            var num = BitConverter.ToUInt32(blob, 0);
            var normalizedValue = num / (float)uint.MaxValue;

            // 범위와 최소값을 이용하여 결과 스케일링
            return normalizedValue * (max - min) + min;
        }

        public static IEnumerable<float> EnumerateSingles<TRng>(this TRng rng, int? count = default, float min = float.MinValue, float max = float.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextSingle(min, max);
        }

        public static double NextDouble<TRng>(this TRng rng, double min = double.MinValue, double max = double.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (min >= max)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            // 안전한 범위로 한정
            if (min < double.MinValue / 2)
                min = double.MinValue / 2;

            if (max > double.MaxValue / 2)
                max = double.MaxValue / 2;

            // 4 바이트 랜덤 바이트 배열 생성
            var blob = new byte[4];
            rng.GetBytes(blob);

            // uint로 변환하여 0과 1 사이의 부동 소수점 수로 스케일링
            var num = BitConverter.ToUInt32(blob, 0);
            var normalizedValue = num / (double)uint.MaxValue;

            // 범위와 최소값을 이용하여 결과 스케일링
            return normalizedValue * (max - min) + min;
        }

        public static IEnumerable<double> EnumerateDoubles<TRng>(this TRng rng, int? count = default, double min = double.MinValue, double max = double.MaxValue)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextDouble(min, max);
        }

        public static decimal NextDecimal<TRng>(this TRng rng, decimal min = -1E+28m, decimal max = 1E+28m)
            where TRng : RandomNumberGenerator
        {
            try
            {
                if (min >= max)
                    throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

                var range = max - min;
                var scale = rng.NextByte(0, 29);  // 소수점 이하 자릿수 결정
                var sign = rng.NextBoolean();  // 부호 결정

                // 세 개의 int 값을 무작위로 생성
                var int1 = rng.NextInt32();
                var int2 = rng.NextInt32();
                var int3 = rng.NextInt32();

                // 랜덤 decimal 생성
                var midValue = new decimal(int1, int2, int3, sign, scale);

                // 범위 조정
                return min + Math.Abs(midValue % range);
            }
            catch (OverflowException thrownException)
            {
                throw new ArgumentException(
                    ErrorMessages.RandomNumberGeneratorExtensions_RangeBetweenMinMaxTooLarge,
                    nameof(max), thrownException);
            }
        }

        public static IEnumerable<decimal> EnumerateDecimals<TRng>(this TRng rng, int? count = default, decimal min = -1E+28m, decimal max = 1E+28m)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextDecimal(min, max);
        }

        public static DateTime NextDateTime<TRng>(this TRng rng, DateTime? min = null, DateTime? max = null)
            where TRng : RandomNumberGenerator
        {
            var minValue = min ?? DateTime.MinValue;
            var maxValue = max ?? DateTime.MaxValue;

            if (minValue >= maxValue)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var timespan = maxValue - minValue;
            var blob = new byte[8];
            rng.GetBytes(blob);
            var ticks = BitConverter.ToUInt64(blob, 0) % (ulong)timespan.Ticks;

            return minValue + new TimeSpan((long)ticks);
        }

        public static IEnumerable<DateTime> EnumerateDateTimes<TRng>(this TRng rng, int? count = default, DateTime? min = null, DateTime? max = null)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextDateTime(min, max);
        }

        public static DateTimeOffset NextDateTimeOffset<TRng>(this TRng rng, DateTimeOffset? min = null, DateTimeOffset? max = null)
            where TRng : RandomNumberGenerator
        {
            var minValue = min ?? DateTimeOffset.MinValue;
            var maxValue = max ?? DateTimeOffset.MaxValue;

            if (minValue >= maxValue)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var timespan = maxValue - minValue;
            var blob = new byte[8];
            rng.GetBytes(blob);
            var ticks = BitConverter.ToUInt64(blob, 0) % (ulong)timespan.Ticks;

            return minValue + new TimeSpan((long)ticks);
        }

        public static IEnumerable<DateTimeOffset> EnumerateDateTimeOffsets<TRng>(this TRng rng, int? count = default, DateTimeOffset? min = null, DateTimeOffset? max = null)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextDateTimeOffset(min, max);
        }

        public static TimeSpan NextTimeSpan<TRng>(this TRng rng, TimeSpan? min = null, TimeSpan? max = null)
            where TRng : RandomNumberGenerator
        {
            var minValue = min ?? default(TimeSpan);  // Assuming a logical minimum value
            var maxValue = max ?? TimeSpan.FromDays(365 * 100);  // A default maximum of 100 years

            if (minValue >= maxValue)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinMustBeLessThanMax, nameof(min));

            var timespan = maxValue - minValue;
            var blob = new byte[8];
            rng.GetBytes(blob);
            var ticks = BitConverter.ToUInt64(blob, 0) % (ulong)timespan.Ticks;

            return minValue + new TimeSpan((long)ticks);
        }

        public static IEnumerable<TimeSpan> EnumerateTimeSpans<TRng>(this TRng rng, int? count = default, TimeSpan? min = null, TimeSpan? max = null)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextTimeSpan(min, max);
        }

        public static Guid NextGuid<TRng>(this TRng rng)
            where TRng : RandomNumberGenerator
        {
            var blob = new byte[16];
            rng.GetBytes(blob);
            return new Guid(blob);
        }

        public static IEnumerable<Guid> EnumerateGuids<TRng>(this TRng rng, int? count = default)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextGuid();
        }

        public static Version NextVersion<TRng>(this TRng rng, Version? min = default, Version? max = default)
            where TRng : RandomNumberGenerator
        {
            if (min == default)
                min = new Version(0, 0, 0, 0);

            if (max == default)
                max = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);

            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Min version must be less than or equal to max version.");

            // 랜덤으로 버전의 각 부분을 생성합니다.
            int major = rng.NextInt32(min.Major, max.Major);
            int minor = rng.NextInt32(min.Minor, max.Minor);
            int build = rng.NextInt32(min.Build, max.Build);
            int revision = rng.NextInt32(min.Revision, max.Revision);

            return new Version(major, minor, build, revision);
        }

        public static IEnumerable<Version> EnumerateVersions<TRng>(this TRng rng, int? count = default, Version? min = default, Version? max = default)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextVersion(min, max);
        }

        public static IPAddress NextIPv4Address<TRng>(this TRng rng)
            where TRng : RandomNumberGenerator
        {
            var bytes = rng.NextByteArray(4);
            bytes[0] = bytes[0] < 1 ? (byte)1 : bytes[0];
            return new IPAddress(bytes);
        }

        public static IEnumerable<IPAddress> EnumerateIPv4Addresses<TRng>(this TRng rng, int? count = default)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextIPv4Address();
        }

        public static IPAddress NextIPv6Address<TRng>(this TRng rng, long ipv6ScopeId = 0L)
            where TRng : RandomNumberGenerator
            => new IPAddress(rng.NextByteArray(16), ipv6ScopeId);

        public static IEnumerable<IPAddress> EnumerateIPv6Addresses<TRng>(this TRng rng, int? count = default, long ipv6ScopeId = 0L)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextIPv6Address(ipv6ScopeId);
        }

        public static IPEndPoint NextIPv4EndPoint<TRng>(this TRng rng)
            where TRng : RandomNumberGenerator
            => new IPEndPoint(rng.NextIPv4Address(), rng.NextPortNumber());

        public static IEnumerable<IPEndPoint> EnumerateIPv4EndPoints<TRng>(this TRng rng, int? count = default)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextIPv4EndPoint();
        }

        public static IPEndPoint NextIPv6EndPoint<TRng>(this TRng rng, long ipv6ScopeId = 0L)
            where TRng : RandomNumberGenerator
            => new IPEndPoint(rng.NextIPv6Address(ipv6ScopeId), rng.NextPortNumber());

        public static IEnumerable<IPEndPoint> EnumerateIPv6EndPoints<TRng>(this TRng rng, int? count = default, long ipv6ScopeId = 0L)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextIPv6EndPoint(ipv6ScopeId);
        }

        public static int NextPortNumber<TRng>(this TRng rng)
            where TRng : RandomNumberGenerator
            => 49152 + (rng.NextUInt16() % (65535 - 49152 + 1));

        public static IEnumerable<int> EnumeratePortNumbers<TRng>(this TRng rng, int? count = default)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextPortNumber();
        }

        public static string NextBase64<TRng>(this TRng rng, int minLength = 16, int maxLength = 512)
            where TRng : RandomNumberGenerator
        {
            if (minLength > maxLength)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_MinLengthMustBeLessThanMaxLength, nameof(minLength));

            // minLength와 maxLength 사이에서 랜덤 길이를 선택
            var length = rng.NextInt32(minLength, maxLength + 1);

            var blob = new byte[length];
            rng.GetBytes(blob);
            return Convert.ToBase64String(blob);
        }

        public static IEnumerable<string> EnumerateBase64<TRng>(this TRng rng, int? count = default, int minLength = 16, int maxLength = 512)
            where TRng : RandomNumberGenerator
        {
            if (count.HasValue && count < 0)
                throw new ArgumentException(ErrorMessages.RandomNumberGeneratorExtensions_CountCannotBeNegative, nameof(count));

            var iterationCount = 0;
            while (!count.HasValue || iterationCount++ < count)
                yield return rng.NextBase64(minLength, maxLength);
        }

        public static byte[] NextByteArray<TRng>(this TRng rng, int length)
            where TRng : RandomNumberGenerator
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "Length cannot be negative number.");

            var blob = new byte[length];
            rng.GetBytes(blob);
            return blob;
        }
    }
#endif // NETSTANDARD1_3_OR_GREATER

    public static partial class RngExtensions { }
}
