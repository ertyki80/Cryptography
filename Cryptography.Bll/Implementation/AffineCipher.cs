using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cryptography.Bll.Models;

namespace Cryptography.Bll.Implementation
{
    public class AffineCipher
    {
        private string _textForSearch;
        private readonly char[] _alphabetEnglishLover = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly char[] _alphabetEnglishUpper = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] cipherArr_en;
        private char[] cipherArr_EN;
        private readonly int[] alphaArr = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };
        private readonly int[] betaArr = Enumerable.Range(0, 26).ToArray();
        public void Initialize(int alpha, int beta)
        {
            _textForSearch =  File.ReadAllText( "../Cryptography.Bll/DictionaryEng/words.txt");
            cipherArr_en = (char[])_alphabetEnglishLover.Clone();
            cipherArr_EN = (char[])(_alphabetEnglishUpper.Clone());
            for (int i = 0; i < _alphabetEnglishLover.Length; i++)
            {
                cipherArr_en[(i * alpha + beta) % _alphabetEnglishLover.Length] = _alphabetEnglishLover[i];
                cipherArr_EN[(i * alpha + beta) % _alphabetEnglishUpper.Length] = _alphabetEnglishUpper[i];
            }

        }
        public string Decode(string codeStream)
        {
            string code = codeStream;
            string decoded = "";
            for (int i = 0; i < code.Length; i++)
            {
                if (_alphabetEnglishLover.Contains(code[i]) || _alphabetEnglishUpper.Contains(code[i]))
                {
                    decoded += cipherArr_en.Contains(code[i]) ? _alphabetEnglishLover[cipherArr_en.ToList().IndexOf(code[i])] : _alphabetEnglishUpper[cipherArr_EN.ToList().IndexOf(code[i])];
                }
                else
                {
                    decoded += code[i];
                }
            }
            return decoded;
        }

        public string Encode(string textStream)
        {
            string text = textStream;
            string encoded = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (_alphabetEnglishLover.Contains(text[i]) || _alphabetEnglishUpper.Contains(text[i]))
                {
                    encoded += _alphabetEnglishLover.Contains(text[i]) ? cipherArr_en[_alphabetEnglishLover.ToList().IndexOf(text[i])] : cipherArr_EN[_alphabetEnglishUpper.ToList().IndexOf(text[i])];
                }
                else
                {
                    encoded += text[i];
                }
            }
            return encoded;
        }
        
        public async Task<List<AffineCipherBruteForce>> BruteForce(string plainText)
        {
            List<AffineCipherBruteForce> bruteForces = new List<AffineCipherBruteForce>();
            string decodedText;
            string lowerCaseCode = plainText.ToLower();
            
            foreach (int alpha in alphaArr)
            {
                foreach (int beta in betaArr)
                {
                    
                    calculateCipherArr_en(alpha, beta);
                    decodedText = AnotherDecode(lowerCaseCode);
                    
                    AffineCipherBruteForce result = new()
                    {
                        Content = decodedText,
                        FirstKey = alpha,
                        SecondKey = beta
                    };
                    bruteForces.Add(result);
                    if (await CheckWords(decodedText))
                    {
                        List<AffineCipherBruteForce> rightResult = new List<AffineCipherBruteForce>();
                        rightResult.Add(result);
                        return rightResult;
                    }
                    
                }
            }
            return bruteForces;
        }
        
        private void calculateCipherArr_en(int alpha, int beta)
        {
            this.cipherArr_en = (char[])(_alphabetEnglishLover.Clone());
            for (int i = 0; i < _alphabetEnglishLover.Length; i++)
            {
                cipherArr_en[(i * alpha + beta) % _alphabetEnglishLover.Length] = _alphabetEnglishLover[i];
            }
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

                if (rightCounter >= input.Split(" ").Length)
                {
                    return true;
                }
            }

            return false;
        }
        private string AnotherDecode(string lowerCaseCode)
        {
            string decodedText = "";
            for (int i = 0; i < lowerCaseCode.Length; i++)
            {
                if (_alphabetEnglishLover.Contains(lowerCaseCode[i]))
                {
                    decodedText += _alphabetEnglishLover[cipherArr_en.ToList().IndexOf(lowerCaseCode[i])];
                }
                else
                {
                    decodedText += lowerCaseCode[i];
                }
            }
            return decodedText;
        }
    }
}