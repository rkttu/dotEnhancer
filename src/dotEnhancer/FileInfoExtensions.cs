using System.Threading.Tasks;
using System.Threading;

namespace dotEnhancer
{
#if NETSTANDARD1_3_OR_GREATER
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    partial class FileInfoExtensions
    {
        // To Do: Modify the code below for file input and output to not rely on BCLs
        public static void OverwriteAllText(this FileInfo fileInfo, IEnumerable<char> text, Encoding? targetEncoding = default)
        {
            if (targetEncoding != null)
                File.WriteAllText(fileInfo.FullName, new string(text.ToArray()), targetEncoding);
            else
                File.WriteAllText(fileInfo.FullName, new string(text.ToArray()));
        }

        public static void OverwriteAllBytes(this FileInfo fileInfo, IEnumerable<byte> bytes)
            => File.WriteAllBytes(fileInfo.FullName, bytes.ToArray());

        public static void OverwriteAllLines(this FileInfo fileInfo, IEnumerable<IEnumerable<char>> lines, Encoding? targetEncoding = default)
        {
            if (targetEncoding != null)
                File.WriteAllLines(fileInfo.FullName, lines.Select(x => new string(x.ToArray())).ToArray(), targetEncoding);
            else
                File.WriteAllLines(fileInfo.FullName, lines.Select(x => new string(x.ToArray())).ToArray());
        }

        public static void AppendAllText(this FileInfo fileInfo, IEnumerable<char> text, Encoding? targetEncoding = default)
        {
            if (targetEncoding != null)
                File.AppendAllText(fileInfo.FullName, new string(text.ToArray()), targetEncoding);
            else
                File.AppendAllText(fileInfo.FullName, new string(text.ToArray()));
        }

        public static void AppendAllLines(this FileInfo fileInfo, IEnumerable<IEnumerable<char>> lines, Encoding? targetEncoding = default)
        {
            if (targetEncoding != null)
                File.AppendAllLines(fileInfo.FullName, lines.Select(x => new string(x.ToArray())).ToArray(), targetEncoding);
            else
                File.AppendAllLines(fileInfo.FullName, lines.Select(x => new string(x.ToArray())).ToArray());
        }

        private const int MaxBufferSize = 67108864;
        private const int MinRandomSize = 0;
        private const int MaxRandomSize = 1024;

        // Original Source Code: https://github.com/bitbeans/diskdetector-net
        public static bool SecureDelete(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                return false;

            using (var rng = RandomNumberGenerator.Create())
            using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                for (var size = fileStream.Length; size > 0; size -= MaxBufferSize)
                {
                    var bufferSize = (size < MaxBufferSize) ? size : MaxBufferSize;
                    var buffer = rng.NextByteArray(bufferSize);

                    fileStream.Write(buffer, 0, buffer.Length);
                    fileStream.Flush(true);
                }

                fileStream.SetLength(rng.NextInt32(MinRandomSize, MaxRandomSize));
            }

            fileInfo.Delete();
            return true;
        }

        private static void TurnOffContentIndexing(this FileSystemInfo fileSystemInfo)
            => fileSystemInfo.Attributes |= FileAttributes.NotContentIndexed;

        public static async Task<bool> SecureDeleteAsync(this FileInfo fileInfo, CancellationToken cancellationToken = default)
        {
            if (!fileInfo.Exists)
                return false;

            fileInfo.TurnOffContentIndexing();

            using (var rng = RandomNumberGenerator.Create())
            using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                for (var size = fileStream.Length; size > 0; size -= MaxBufferSize)
                {
                    var bufferSize = (size < MaxBufferSize) ? size : MaxBufferSize;
                    var buffer = rng.NextByteArray(bufferSize);

                    await fileStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                    await fileStream.FlushAsync(cancellationToken).ConfigureAwait(false);
                }

                fileStream.SetLength(rng.NextInt32(MinRandomSize, MaxRandomSize));
            }

            return true;
        }
    }
#endif // NETSTANDARD1_3_OR_GREATER

#if NETSTANDARD1_4_OR_GREATER
    partial class FileInfoExtensions
    {
        private static readonly DateTime EpochMinDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
        private static readonly DateTime EpochMaxDateTime = EpochMinDateTime.AddSeconds(int.MaxValue);

        public static bool SecureDelete(this FileInfo fileInfo, bool obfuscateFileDateTime)
        {
            if (!fileInfo.Exists)
                return false;

            fileInfo.TurnOffContentIndexing();

            if (obfuscateFileDateTime)
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    var obfuscatedCreationTime = rng.NextDateTime(EpochMinDateTime, EpochMaxDateTime);
                    var obfuscatedLastAccessTime = rng.NextDateTime(EpochMinDateTime, EpochMaxDateTime);
                    var obfuscatedLastWriteTime = rng.NextDateTime(EpochMinDateTime, EpochMaxDateTime);

                    fileInfo.CreationTime = obfuscatedCreationTime;
                    fileInfo.LastAccessTime = obfuscatedLastAccessTime;
                    fileInfo.LastWriteTime = obfuscatedLastWriteTime;
                }
            }

            return SecureDelete(fileInfo);
        }

        public static async Task<bool> SecureDeleteAsync(this FileInfo fileInfo, bool obfuscateFileDateTime = true, CancellationToken cancellationToken = default)
        {
            if (obfuscateFileDateTime)
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    var obfuscatedCreationTime = rng.NextDateTime(EpochMinDateTime, EpochMaxDateTime);
                    var obfuscatedLastAccessTime = rng.NextDateTime(EpochMinDateTime, EpochMaxDateTime);
                    var obfuscatedLastWriteTime = rng.NextDateTime(EpochMinDateTime, EpochMaxDateTime);

                    fileInfo.CreationTime = obfuscatedCreationTime;
                    fileInfo.LastAccessTime = obfuscatedLastAccessTime;
                    fileInfo.LastWriteTime = obfuscatedLastWriteTime;
                }
            }

            return await SecureDeleteAsync(fileInfo, cancellationToken).ConfigureAwait(false);
        }
    }
#endif // NETSTANRDARD1_4_OR_GREATER

    internal static partial class FileInfoExtensions { }
}
