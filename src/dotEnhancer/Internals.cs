namespace dotEnhancer
{
#if NETSTANDARD1_3_OR_GREATER
    using System;
    using System.IO;
    using System.Security.Cryptography;

    internal static class Internals
    {
        private static readonly DateTime MinEpochDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
        private static readonly DateTime MaxEpochDateTime = MinEpochDateTime.AddSeconds(int.MaxValue);

        internal static void PerformObfuscation_BeforeOverwrite(FileSystemInfo fileSystemInfo, SecureDeleteObfuscationMode obfuscationMode)
        {
            if (!fileSystemInfo.Exists)
                return;

            var disableContentIndexing = (obfuscationMode & SecureDeleteObfuscationMode.DisableContentIndexing) == SecureDeleteObfuscationMode.DisableContentIndexing;

            if (disableContentIndexing)
            {
                try { fileSystemInfo.Attributes |= FileAttributes.NotContentIndexed; }
                catch { }
            }
        }

        internal static void PerformObfuscation_AfterOverwrite(FileSystemInfo fileSystemInfo, SecureDeleteObfuscationMode obfuscationMode, RandomNumberGenerator rng)
        {
            if (!fileSystemInfo.Exists)
                return;

            var rename = (obfuscationMode & SecureDeleteObfuscationMode.Rename) == SecureDeleteObfuscationMode.Rename;
            var modifyLastAccessTime = (obfuscationMode & SecureDeleteObfuscationMode.ModifyLastAccessTime) == SecureDeleteObfuscationMode.ModifyLastAccessTime;
            var modifyLastWriteTime = (obfuscationMode & SecureDeleteObfuscationMode.ModifyLastWriteTime) == SecureDeleteObfuscationMode.ModifyLastWriteTime;
            var modifyCreationTime = (obfuscationMode & SecureDeleteObfuscationMode.ModifyCreationTime) == SecureDeleteObfuscationMode.ModifyCreationTime;

            if (rename)
            {
                if (fileSystemInfo is DirectoryInfo directoryInfo)
                {
                    var newPath = Path.Combine(directoryInfo.Parent.FullName, Guid.NewGuid().ToString());

                    // This object will be updated to new path if the rename operation is successful.
                    try { directoryInfo.MoveTo(newPath); }
                    catch { }
                }
                else if (fileSystemInfo is FileInfo fileInfo)
                {
                    var newPath = Path.Combine(fileInfo.DirectoryName, Guid.NewGuid().ToString() + ".dat");

                    // This object will be updated to new path if the rename operation is successful.
                    try { fileInfo.MoveTo(newPath); }
                    catch { }
                }
            }

            if (modifyLastAccessTime)
            {
                try { fileSystemInfo.LastAccessTime = rng.NextDateTime(MinEpochDateTime, MaxEpochDateTime); }
                catch { }
            }

            if (modifyLastWriteTime)
            {
                try { fileSystemInfo.LastWriteTime = rng.NextDateTime(MinEpochDateTime, MaxEpochDateTime); }
                catch { }
            }

            if (modifyCreationTime)
            {
                try { fileSystemInfo.CreationTime = rng.NextDateTime(MinEpochDateTime, MaxEpochDateTime); }
                catch { }
            }
        }
    }
#endif // NETSTANDARD1_3_OR_GREATER
}
