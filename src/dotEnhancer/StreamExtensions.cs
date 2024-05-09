namespace dotEnhancer
{
#if NETSTANDARD1_0_OR_GREATER
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    partial class StreamExtensions
    {
        public static void ReadStringWithEncoding<TReadableStream, TEncoding>(this TReadableStream readableStream, TEncoding targetEncoding, Action<ArraySegment<char>> callback, int charBufferSize = 16)
            where TReadableStream : Stream
            where TEncoding : Encoding
            => ReadString(readableStream, callback, targetEncoding.GetDecoder(), charBufferSize);

        public static void ReadString<TReadableStream, TDecoder>(this TReadableStream readableStream, Action<ArraySegment<char>> callback, TDecoder sourceDecoder, int charBufferSize = 16)
            where TReadableStream : Stream
            where TDecoder : Decoder
        {
            if (readableStream == default)
                throw new ArgumentNullException(nameof(readableStream));
            if (sourceDecoder == default)
                throw new ArgumentNullException(nameof(sourceDecoder));
            if (callback == default)
                throw new ArgumentNullException(nameof(callback));
            if (!readableStream.CanRead)
                throw new ArgumentException("Cannot read from the stream because it is not readable.", nameof(readableStream));

            charBufferSize = Math.Max(charBufferSize, 16); // Ensure minimum buffer size
            var byteBuffer = new byte[charBufferSize * 4]; // Assumes max 4 bytes per character
            var charBuffer = new char[charBufferSize];
            var completed = false;

            while (!completed)
            {
                var bytesRead = readableStream.Read(byteBuffer, 0, byteBuffer.Length);
                if (bytesRead == 0) break; // No more data to read

                sourceDecoder.Convert(
                    byteBuffer, 0, bytesRead,
                    charBuffer, 0, charBuffer.Length,
                    false,
                    out int bytesUsed, out int charsUsed, out completed);

                // Call the callback action with the newly decoded string
                if (charsUsed > 0)
                    callback.Invoke(new ArraySegment<char>(charBuffer, 0, charsUsed));

                // Handle the scenario where not all bytes were used (partial character bytes)
                if (bytesUsed < bytesRead)
                    Array.Copy(byteBuffer, bytesUsed, byteBuffer, 0, bytesRead - bytesUsed); // Shift leftover bytes to the beginning
            }
        }

        public static Task ReadStringWithEncodingAsync<TReadableStream, TEncoding>(this TReadableStream readableStream, Func<ArraySegment<char>, CancellationToken, Task> callback, TEncoding targetEncoding, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TReadableStream : Stream
            where TEncoding : Encoding
            => ReadStringAsync(readableStream, callback, targetEncoding.GetDecoder(), charBufferSize, cancellationToken);

        public static async Task ReadStringAsync<TReadableStream, TDecoder>(this TReadableStream readableStream, Func<ArraySegment<char>, CancellationToken, Task> callback, TDecoder sourceDecoder, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TReadableStream : Stream
            where TDecoder : Decoder
        {
            if (readableStream == default)
                throw new ArgumentNullException(nameof(readableStream));
            if (sourceDecoder == default)
                throw new ArgumentNullException(nameof(sourceDecoder));
            if (callback == default)
                throw new ArgumentNullException(nameof(callback));
            if (!readableStream.CanRead)
                throw new ArgumentException("Cannot read from the stream because it is not readable.", nameof(readableStream));

            charBufferSize = Math.Max(charBufferSize, 16); // Ensure minimum buffer size
            var byteBuffer = new byte[charBufferSize * 4]; // Assumes max 4 bytes per character
            var charBuffer = new char[charBufferSize];
            var completed = false;

            while (!completed)
            {
                var bytesRead = await readableStream.ReadAsync(byteBuffer, 0, byteBuffer.Length, cancellationToken).ConfigureAwait(false);
                if (bytesRead == 0) break; // No more data to read

                sourceDecoder.Convert(
                    byteBuffer, 0, bytesRead,
                    charBuffer, 0, charBuffer.Length,
                    false,
                    out int bytesUsed, out int charsUsed, out completed);

                // Call the callback action with the newly decoded string
                if (charsUsed > 0)
                    await callback.Invoke(new ArraySegment<char>(charBuffer, 0, charsUsed), cancellationToken).ConfigureAwait(false);

                // Handle the scenario where not all bytes were used (partial character bytes)
                if (bytesUsed < bytesRead)
                    Array.Copy(byteBuffer, bytesUsed, byteBuffer, 0, bytesRead - bytesUsed); // Shift leftover bytes to the beginning
            }
        }

        public static void WriteStringWithEncoding<TWritableStream, TEncoding>(this TWritableStream writableStream, string value, TEncoding targetEncoding, int charBufferSize = 16)
            where TWritableStream : Stream
            where TEncoding : Encoding
            => WriteString(writableStream, value, targetEncoding.GetEncoder(), charBufferSize);

        public static void WriteString<TWritableStream, TEncoder>(this TWritableStream writableStream, string value, TEncoder targetEncoder, int charBufferSize = 16)
            where TWritableStream : Stream
            where TEncoder : Encoder
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value));

            if (targetEncoder == default)
                throw new ArgumentNullException(nameof(targetEncoder));

            if (writableStream == default)
                throw new ArgumentNullException(nameof(writableStream));

            if (!writableStream.CanWrite)
                throw new ArgumentException("Cannot read the stream because it is not writable.", nameof(writableStream));

            charBufferSize = charBufferSize < 16 ? 16 : charBufferSize;

            var completed = default(bool);
            var charBuffer = new char[charBufferSize];
            var byteBuffer = new byte[charBufferSize * 4];
            var charIndex = default(int);
            var charArray = value.ToCharArray();

            while (!completed)
            {
                int charsToCopy = Math.Min(charBufferSize, value.Length - charIndex);
                Array.Copy(charArray, charIndex, charBuffer, 0, charsToCopy);

                targetEncoder.Convert(
                    charBuffer, 0, charsToCopy,
                    byteBuffer, 0, byteBuffer.Length,
                    charIndex + charsToCopy == value.Length,
                    out int charsUsed, out int bytesUsed, out completed);

                writableStream.Write(byteBuffer, 0, bytesUsed);
                charIndex += charsUsed;
            }
        }

        public static Task WriteStringWithEncodingAsync<TWritableStream, TEncoding>(this TWritableStream writableStream, string value, TEncoding targetEncoding, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            where TEncoding : Encoding
            => WriteStringAsync(writableStream, value, targetEncoding.GetEncoder(), charBufferSize, cancellationToken);

        public static async Task WriteStringAsync<TWritableStream, TEncoder>(this TWritableStream writableStream, string value, TEncoder targetEncoder, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            where TEncoder : Encoder
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value));

            if (targetEncoder == default)
                throw new ArgumentNullException(nameof(targetEncoder));

            if (writableStream == default)
                throw new ArgumentNullException(nameof(writableStream));

            if (!writableStream.CanWrite)
                throw new ArgumentException("Cannot read the stream because it is not writable.", nameof(writableStream));

            charBufferSize = charBufferSize < 16 ? 16 : charBufferSize;

            var completed = default(bool);
            var charBuffer = new char[charBufferSize];
            var byteBuffer = new byte[charBufferSize * 4];
            var charIndex = default(int);
            var charArray = value.ToCharArray();

            while (!completed)
            {
                int charsToCopy = Math.Min(charBufferSize, value.Length - charIndex);
                Array.Copy(charArray, charIndex, charBuffer, 0, charsToCopy);

                targetEncoder.Convert(
                    charBuffer, 0, charsToCopy,
                    byteBuffer, 0, byteBuffer.Length,
                    charIndex + charsToCopy == value.Length,
                    out int charsUsed, out int bytesUsed, out completed);

                await writableStream.WriteAsync(byteBuffer, 0, bytesUsed, cancellationToken).ConfigureAwait(false);
                charIndex += charsUsed;
            }
        }

        private static readonly Lazy<Encoding> UTF8WithoutBOMEncoding =
            new Lazy<Encoding>(() => new UTF8Encoding(false));

        public static void ReadStringAsUTF8WithoutBOMBytes<TReadableStream>(this TReadableStream readableStream, Action<ArraySegment<char>> callback, int charBufferSize = 16)
            where TReadableStream : Stream
            => ReadStringWithEncoding(readableStream, UTF8WithoutBOMEncoding.Value, callback, charBufferSize);

        public static void WriteStringAsUTF8WithoutBOMBytes<TWritableStream>(this TWritableStream writableStream, string value, int charBufferSize = 16)
            where TWritableStream : Stream
            => WriteStringWithEncoding(writableStream, value, UTF8WithoutBOMEncoding.Value, charBufferSize);

        public static Task ReadStringAsUTF8WithoutBOMBytesAsync<TReadableStream>(this TReadableStream readableStream, Func<ArraySegment<char>, CancellationToken, Task> callback, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TReadableStream : Stream
            => ReadStringWithEncodingAsync(readableStream, callback, UTF8WithoutBOMEncoding.Value, charBufferSize, cancellationToken);

        public static Task WriteStringAsUTF8WithoutBOMBytesAsync<TWritableStream>(this TWritableStream writableStream, string value, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            => WriteStringWithEncodingAsync(writableStream, value, UTF8WithoutBOMEncoding.Value, charBufferSize, cancellationToken);

        public static void ReadStringAsUTF8Bytes<TReadableStream>(this TReadableStream readableStream, Action<ArraySegment<char>> callback, int charBufferSize = 16)
            where TReadableStream : Stream
            => ReadStringWithEncoding(readableStream, Encoding.UTF8, callback, charBufferSize);

        public static void WriteStringAsUTF8Bytes<TWritableStream>(this TWritableStream writableStream, string value, int charBufferSize = 16)
            where TWritableStream : Stream
            => WriteStringWithEncoding(writableStream, value, Encoding.UTF8, charBufferSize);

        public static Task ReadStringAsUTF8BytesAsync<TReadableStream>(this TReadableStream readableStream, Func<ArraySegment<char>, CancellationToken, Task> callback, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TReadableStream : Stream
            => ReadStringWithEncodingAsync(readableStream, callback, Encoding.UTF8, charBufferSize, cancellationToken);

        public static Task WriteStringAsUTF8BytesAsync<TWritableStream>(this TWritableStream writableStream, string value, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            => WriteStringWithEncodingAsync(writableStream, value, Encoding.UTF8, charBufferSize, cancellationToken);

        public static void ReadStringAsEncodedBytes<TReadableStream>(this TReadableStream readableStream, Action<ArraySegment<char>> callback, string encodingName, int charBufferSize = 16)
            where TReadableStream : Stream
            => ReadStringWithEncoding(readableStream, Encoding.GetEncoding(encodingName), callback, charBufferSize);

        public static void WriteStringAsEncodedBytes<TWritableStream>(this TWritableStream writableStream, string value, string encodingName, int charBufferSize = 16)
            where TWritableStream : Stream
            => WriteStringWithEncoding(writableStream, value, Encoding.GetEncoding(encodingName), charBufferSize);

        public static Task ReadStringAsEncodedBytesAsync<TReadableStream>(this TReadableStream readableStream, Func<ArraySegment<char>, CancellationToken, Task> callback, string encodingName, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TReadableStream : Stream
            => ReadStringWithEncodingAsync(readableStream, callback, Encoding.GetEncoding(encodingName), charBufferSize);

        public static void WriteStringAsEncodedBytesAsync<TWritableStream>(this TWritableStream writableStream, string value, string encodingName, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            => WriteStringWithEncodingAsync(writableStream, value, Encoding.GetEncoding(encodingName), charBufferSize, cancellationToken);
    }
#endif // NETSTANDARD1_0_OR_GREATER

#if NETSTANDARD1_3_OR_GREATER
    partial class StreamExtensions
    {
        public static void WriteStringAsASCIIBytes<TWritableStream>(this TWritableStream writableStream, string value, int charBufferSize = 16)
            where TWritableStream : Stream
            => WriteStringWithEncoding(writableStream, value, Encoding.ASCII, charBufferSize);

        public static Task WriteStringAsASCIIBytesAsync<TWritableStream>(this TWritableStream writableStream, string value, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            => WriteStringWithEncodingAsync(writableStream, value, Encoding.ASCII, charBufferSize, cancellationToken);

        public static void WriteStringAsToEncodedBytes<TWritableStream>(this TWritableStream writableStream, string value, int codePage, int charBufferSize = 16)
            where TWritableStream : Stream
            => WriteStringWithEncoding(writableStream, value, Encoding.GetEncoding(codePage), charBufferSize);

        public static Task WriteStringAsEncodedBytesAsync<TWritableStream>(this TWritableStream writableStream, string value, int codePage, int charBufferSize = 16, CancellationToken cancellationToken = default)
            where TWritableStream : Stream
            => WriteStringWithEncodingAsync(writableStream, value, Encoding.GetEncoding(codePage), charBufferSize, cancellationToken);
    }
#endif // NETSTANDARD1_3_OR_GREATER

    public static partial class StreamExtensions { }
}
