using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Bll.Implementation
{
    public static class AffineCipher
    {
        private const int Alphabet = 26;
        private const int ChunkSize = 5;
        
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
            while(source.Any())
            {
                yield return source.Take(size);
                source = source.Skip(size);
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