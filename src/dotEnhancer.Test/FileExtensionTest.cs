using System.Text;

namespace dotEnhancer.Test;

public class FileExtensionTest
{
    [Fact]
    public void SecureDeleteTest()
    {
        var fileInfo = new FileInfo(Path.GetTempFileName());
        fileInfo.OverwriteAllText("Hello, World!", new UTF8Encoding(false));
        fileInfo.SecureDelete(obfuscateFileDateTime: true);
        Assert.False(fileInfo.Exists);
    }
}
