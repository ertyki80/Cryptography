using System.Collections.Generic;
using Cryptography.Bll.Interfaces;

namespace Cryptography.Bll.Implementation
{
    public class VigenereCipher:IVigenereCipher
    {
        private readonly IList<char> arr_en = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly IList<char> arr_EN = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        

        public string Encode(string key,string text)
        {
            string encoded = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (arr_en.Contains(text[i]))
                {
                    encoded += arr_en[(arr_en.IndexOf(text[i]) + arr_en.IndexOf(key[i % key.Length])) % 26];
                }
                else if (arr_EN.Contains(text[i]))
                {
                    encoded += arr_EN[(arr_EN.IndexOf(text[i]) + arr_en.IndexOf(key[i % key.Length])) % 26];
                }
                else
                {
                    encoded += text[i];
                }
            }
            return encoded;
        }

        public string Decode(string key,string code)
        {
            string decoded = "";
            for (int i = 0; i < code.Length; i++)
            {
                if (arr_en.Contains(code[i]))
                {
                    decoded += arr_en[(arr_en.IndexOf(code[i]) - arr_en.IndexOf(key[i % key.Length]) + 26) % 26];
                }
                else if (arr_EN.Contains(code[i]))
                {
                    decoded += arr_EN[(arr_EN.IndexOf(code[i]) - arr_en.IndexOf(key[i % key.Length]) + 26) % 26];
                }
                else
                {
                    decoded += code[i];
                }
            }
            return decoded;
        }
    }
}