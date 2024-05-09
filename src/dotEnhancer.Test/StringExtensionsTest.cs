#pragma warning disable CS8631

using System.Text;

namespace dotEnhancer.Test
{
    public sealed class StringExtensionsTest
    {
        [Fact]
        public void Test_EnsureNotNull()
        {
            var value = default(string);
            Assert.Throws<ArgumentNullException>(() => value.EnsureNotNull());
        }

        [Fact]
        public void Test_EnsureNotNullOrEmpty()
        {
            var value = default(string);
            Assert.Throws<ArgumentNullException>(() => value.EnsureNotNullOrEmpty());

            value = string.Empty;
            Assert.Throws<ArgumentException>(() => value.EnsureNotNullOrEmpty());
        }

        [Fact]
        public void Test_ToBytes()
        {
            var value = "Hello, World!";
            var targetEncoding = new UTF8Encoding(false);
            using var writableStream = new MemoryStream();
            writableStream.WriteStringWithEncoding(value, targetEncoding);
            Assert.Equal(targetEncoding.GetBytes(value), writableStream.ToArray());
        }

        [Fact]
        public void Test_ToEncodedBytes()
        {
            var value = "Hello, World!";
            var targetEncoding = Encoding.UTF8;
            using var writableStream = new MemoryStream();
            writableStream.WriteString(value, targetEncoding.GetEncoder());
            Assert.Equal(targetEncoding.GetBytes(value), writableStream.ToArray());
        }
    }
}
