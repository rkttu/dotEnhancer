namespace dotEnhancer
{
#if NETSTANDARD1_3_OR_GREATER
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

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
        public static void SecureDelete(this FileInfo fileInfo, int repeatCount = 3, SecureDeleteObfuscationMode obfuscationMode = SecureDeleteObfuscationMode.All)
        {
            if (!fileInfo.Exists)
                return;

            if (repeatCount < 1)
                repeatCount = 1;

            using (var rng = RandomNumberGenerator.Create())
            {
                try
                {
                    Internals.PerformObfuscation_BeforeOverwrite(fileInfo, obfuscationMode);

                    var fillRandomData = (obfuscationMode & SecureDeleteObfuscationMode.FillWithRandomData) == SecureDeleteObfuscationMode.FillWithRandomData;
                    var modifyFileSize = (obfuscationMode & SecureDeleteObfuscationMode.ModifyFileSize) == SecureDeleteObfuscationMode.ModifyFileSize;

                    using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Write, FileShare.None))
                    {
                        for (var i = 0; i < repeatCount; i++)
                        {
                            for (var size = fileStream.Length; size > 0; size -= MaxBufferSize)
                            {
                                var bufferSize = (size < MaxBufferSize) ? size : MaxBufferSize;
                                var buffer = fillRandomData ? rng.NextByteArray(bufferSize) : new byte[bufferSize];

                                fileStream.Write(buffer, 0, buffer.Length);
                                fileStream.Flush(true);
                            }

                            fileStream.Seek(0L, SeekOrigin.Begin);
                        }

                        if (modifyFileSize)
                        {
                            try { fileStream.SetLength(rng.NextInt32(MinRandomSize, MaxRandomSize)); }
                            catch { }
                        }
                    }

                    Internals.PerformObfuscation_AfterOverwrite(fileInfo, obfuscationMode, rng);
                }
                catch { }
                finally { fileInfo.Delete(); }
            }
        }

        public static async Task SecureDeleteAsync(this FileInfo fileInfo, int repeatCount = 3, SecureDeleteObfuscationMode obfuscationMode = SecureDeleteObfuscationMode.All, CancellationToken cancellationToken = default)
        {
            if (!fileInfo.Exists)
                return;

            if (repeatCount < 1)
                repeatCount = 1;

            using (var rng = RandomNumberGenerator.Create())
            {
                try
                {
                    Internals.PerformObfuscation_BeforeOverwrite(fileInfo, obfuscationMode);

                    var fillRandomData = (obfuscationMode & SecureDeleteObfuscationMode.FillWithRandomData) == SecureDeleteObfuscationMode.FillWithRandomData;
                    var modifyFileSize = (obfuscationMode & SecureDeleteObfuscationMode.ModifyFileSize) == SecureDeleteObfuscationMode.ModifyFileSize;

                    using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Write, FileShare.None))
                    {
                        for (var i = 0; i < repeatCount; i++)
                        {
                            for (var size = fileStream.Length; size > 0; size -= MaxBufferSize)
                            {
                                var bufferSize = (size < MaxBufferSize) ? size : MaxBufferSize;
                                var buffer = fillRandomData ? rng.NextByteArray(bufferSize) : new byte[bufferSize];

                                await fileStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                                await fileStream.FlushAsync(cancellationToken).ConfigureAwait(false);
                            }

                            fileStream.Seek(0L, SeekOrigin.Begin);
                        }

                        if (modifyFileSize)
                        {
                            try { fileStream.SetLength(rng.NextInt32(MinRandomSize, MaxRandomSize)); }
                            catch { }
                        }
                    }

                    Internals.PerformObfuscation_AfterOverwrite(fileInfo, obfuscationMode, rng);
                }
                catch { }
                finally { fileInfo.Delete(); }
            }
        }
    }
#endif // NETSTANDARD1_3_OR_GREATER

    public static partial class FileInfoExtensions { }
}
