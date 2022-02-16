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
            string inputPath = "../Cryptography.Bll/DictionaryEng/words.txt";
            _textForSearch =  File.ReadAllText(inputPath);
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

        public async Task<List<BruteForceModel>> BruteForce(string input)
        {
            List<BruteForceModel> fullScan = new List<BruteForceModel>();
            List<BruteForceModel> rightAnswer = new List<BruteForceModel>();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();

            for (int shift = 0; shift < letters.Length; shift++)
            {
                StringBuilder stringBuilder = new StringBuilder();
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

                BruteForceModel model;
                var currentWord = stringBuilder.ToString();
                
                if (await CheckWords(currentWord))
                {
                    model = new BruteForceModel()
                    {
                        Content = currentWord,
                        ShiftChar = shift
                    };
                    rightAnswer.Add(model);
                    break;
                }
               
                model = new BruteForceModel()
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
            int wordCounter = 0;
            int rightCounter = 0;
            foreach (string s in input.Split(" "))
            {
                wordCounter++;

                if (_textForSearch.Contains(s.ToLower()))
                {
                    rightCounter++;
                }

                if (wordCounter == 20 && rightCounter > 15)
                {
                    return true;
                }
            }

            return false;
        }
    }
}