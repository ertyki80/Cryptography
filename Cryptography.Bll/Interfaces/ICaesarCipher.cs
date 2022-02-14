namespace Cryptography.Bll.Interfaces
{
    public interface ICaesarCipher
    {
        string Encipher(string input, int key);
        string Decipher(string input, int key);
    }
}