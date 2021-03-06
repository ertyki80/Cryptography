using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptography.Bll.Interfaces;
using Cryptography.Bll.Models;

namespace Cryptography.Bll.Implementation
{
    public class CaesarCipher : ICaesarCipher
    {
        private string _textForSearch;
        public  CaesarCipher()
        {
            _textForSearch =  File.ReadAllText( "../Cryptography.Bll/DictionaryEng/words.txt");
        }
        private static char Cipher(char inputChar, int key)
        {
            if (!char.IsLetter(inputChar))
                return inputChar;

            char offset = char.IsUpper(inputChar) ? 'A' : 'a';
            return (char) ((inputChar + key - offset) % 26 + offset);
        }

        public string Encipher(string input, int key)
        {
            return input.Aggregate(string.Empty, (current, ch) => current + Cipher(ch, key));
        }

        public string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }

        public async Task<List<CaesarCipherBruteForceModel>> BruteForce(string input)
        {
            List<CaesarCipherBruteForceModel> fullScan = new List<CaesarCipherBruteForceModel>();
            List<CaesarCipherBruteForceModel> rightAnswer = new List<CaesarCipherBruteForceModel>();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();

            for (int shift = 0; shift < letters.Length; shift++)
            {
                StringBuilder stringBuilder = new();
                foreach (char inputChar in input.ToLower())
                {
                    if (letters.Contains(inputChar))
                    {
                        int num = letters.IndexOf(inputChar);
                        num += shift;

                        if (num >= letters.Length)
                            num -= letters.Length;

                        stringBuilder = stringBuilder.Append(letters[num]);
                    }
                    else
                    {
                        stringBuilder = stringBuilder.Append(inputChar);
                    }
                }

                CaesarCipherBruteForceModel model;
                var currentWord = stringBuilder.ToString();
                
                if (await CheckWords(currentWord))
                {
                    model = new CaesarCipherBruteForceModel()
                    {
                        Content = currentWord,
                        ShiftChar = shift
                    };
                    rightAnswer.Add(model);
                    break;
                }
               
                model = new CaesarCipherBruteForceModel()
                {
                    Content = currentWord,
                    ShiftChar = shift
                };
                fullScan.Add(model);
            }

            if (rightAnswer.Count != 0)
            {
                return rightAnswer;
            }

            return fullScan;
        }

        private async Task<bool> CheckWords(string input)
        {
            int rightCounter = 0;
            foreach (string s in input.Split(" "))
            {

                if (_textForSearch.Contains(s.ToLower()))
                {
                    rightCounter++;
                }

                if (rightCounter >= input.Split(" ").Length / 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}