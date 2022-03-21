using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cryptography.Bll.Models;

namespace Cryptography.Bll.Implementation
{
    public static class AffineCipher
    {
        private static string _textForSearch;
        private const int Alphabet = 26;
        private const int ChunkSize = 5;

        static AffineCipher()
        {
            _textForSearch =  File.ReadAllText( "../Cryptography.Bll/DictionaryEng/words.txt");
        }
        
        public static string Encode(string plainText, int a, int b)
        {
            if(!IsCoprime(a, Alphabet)) 
                throw new ArgumentException("Numbers must be coprime");
            var encoded = plainText.ToLower().Where(char.IsLetterOrDigit)
                .Select(x => char.IsDigit(x) ? x : ToChar((a * Index(x) + b) % Alphabet));
            
            return string.Join(" ", encoded.Chunk(ChunkSize).Select(x => new string(x.ToArray())));
        }
        private static IEnumerable<IEnumerable<char>> Chunk(this IEnumerable<char> source, int size)
        {
            var enumerable = source as char[] ?? source.ToArray();
            while(enumerable.Any())
            {
                yield return enumerable.Take(size);
                source = enumerable.Skip(size);
            }
        }
        public static string Decode(string cipheredText, int a, int b)
        {
            if(!IsCoprime(a, Alphabet))
                throw new ArgumentException("Numbers must be coprime");
            return new string(cipheredText.Where(char.IsLetterOrDigit)
                .Select(x => char.IsDigit(x) ? x : DecodeSymbol(x, a, b))
                .ToArray());
        }

        public static List<AffineCipherBruteForce> BruteForce(string cipherText)
        {
            List<AffineCipherBruteForce> result = new List<AffineCipherBruteForce>();
            for (int i = 1; i < 26; i++)
            {
                for (int j = 1; j < 26; j++)
                {
                    if (!IsCoprime(i, Alphabet)) continue;
                    string decodedText = Encode(cipherText, i, j);
                    result.Add(
                        new AffineCipherBruteForce
                        {
                            Content = decodedText,
                            FirstKey = i,
                            SecondKey = j
                        });
                    if (CheckWords(decodedText))
                    {
                        return result;
                    }
                }
            }
            return result;
        }
        
        private static bool CheckWords(string input)
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
        
        private static char DecodeSymbol(char c, int a, int b) 
        {
            var value = Mmi(a) * (Index(c) - b) % Alphabet;
            if(value < 0) value += Alphabet;
            return ToChar(value);
        }
        private static int Mmi(int a) => Enumerable.Range(1, Alphabet).First(x => (a * x % Alphabet) == 1);
        private static int Index(char c) => c - 'a';
        private static char ToChar(int i) => (char)('a' + i);
        private static bool IsCoprime(int a, int m) => a == 0 ? m == 1 : IsCoprime(m % a, a);
    }
}