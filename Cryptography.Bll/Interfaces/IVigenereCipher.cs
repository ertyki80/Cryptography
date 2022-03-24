namespace Cryptography.Bll.Interfaces
{
    public interface IVigenereCipher
    {
        string Encode(string key,string text);
        string Decode(string key,string code);
    }
}