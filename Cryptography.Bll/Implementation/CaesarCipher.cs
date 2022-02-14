using System.Linq;
using Cryptography.Bll.Interfaces;

namespace Cryptography.Bll.Implementation
{
    public class CaesarCipher: ICaesarCipher
    {
        private static char Cipher(char inputChar, int key)
        {
            if (!char.IsLetter(inputChar))
                return inputChar;

            char offset = char.IsUpper(inputChar) ? 'A' : 'a';
            return (char)((inputChar + key - offset) % 26 + offset);
        }

        public string Encipher(string input, int key)
        {
            return input.Aggregate(string.Empty, (current, ch) => current + Cipher(ch, key));
        }

        public string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }
    }
}