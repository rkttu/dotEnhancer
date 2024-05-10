namespace dotEnhancer
{
    using System;

    [Flags]
    public enum SecureDeleteObfuscationMode : int
    {
        None =
            0 << 0,
        FillWithRandomData =
            1 << 0,
        DisableContentIndexing =
            1 << 1,
        ModifyLastWriteTime =
            1 << 2,
        ModifyLastAccessTime =
            1 << 3,
        ModifyCreationTime =
            1 << 4,
        ModifyFileSize =
            1 << 5,
        Rename =
            1 << 6,
        All =
            (1 << 7) - 1,
    }
}
