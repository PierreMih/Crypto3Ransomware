namespace Crypto3Ransomware;

public static class FileDecrypter
{
    private const int BlocSize = 128;
    private const int Offset = 0;
    public static async Task DecryptAndWriteToPath(string fileToDecryptPath, string pathToWriteDecryptedFile)
    {
        using var sourceStream =
            new FileStream(
                fileToDecryptPath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: BlocSize, useAsync: true);

        var bytes = new byte[0x1000];
        int numRead;
        // ConcurrentBag<byte[]> bytesEncrypted = [];
        var aesDecryptor = new MyAes().GetDecryptor();
        
        using var outputStream = new FileStream(pathToWriteDecryptedFile, FileMode.Append, FileAccess.Write,
            FileShare.ReadWrite, bufferSize: BlocSize, useAsync: true);

        while ((numRead = await sourceStream.ReadAsync(bytes, Offset, bytes.Length)) != 0)
        {
            var hashedBytes = aesDecryptor.TransformFinalBlock(bytes, 0,BlocSize);
            var cancellationToken = new CancellationToken();
            await outputStream.WriteAsync(hashedBytes, cancellationToken);
        }

        
    }
}