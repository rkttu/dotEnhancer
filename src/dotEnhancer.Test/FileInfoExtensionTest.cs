using System.Text;

namespace dotEnhancer.Test;

public class FileInfoExtensionTest
{
    [Fact]
    public void SecureDeleteTest()
    {
        var fileInfo = new FileInfo(Path.GetTempFileName());
        fileInfo.OverwriteAllText("Hello, World!", new UTF8Encoding(false));
        fileInfo.SecureDelete(5, SecureDeleteObfuscationMode.All);
        Assert.False(fileInfo.Exists);
    }
}
