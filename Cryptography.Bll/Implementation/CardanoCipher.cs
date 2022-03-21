using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Cryptography.Bll.Extensions;
using Cryptography.Common.Enums;
using Cryptography.Common.Models;

namespace Cryptography.Bll.Implementation
{
    public class CardanoCipher
    {
         public static Cipher GenerateCardanoGrid(int cipherLength, RotationMethod rotationMethod, GridType gridType)
        {
            int edge1=0;
            int edge2=0;

            int newMessageLength = 0;

            switch (gridType)
            {
                case GridType.Square:
                    var edge = Math.Sqrt(cipherLength);
                    if (edge % 1 != 0)
                    {
                        foreach (var square in Enumerable.Range(2, 100).Where((x) => x % 2 == 0)
                            .Select(x => new long[] {x, x * x}))
                        {
                            if (square[1] <= cipherLength) continue;

                            edge = square[0];
                            break;
                        }
                    }

                    if (edge % 2 != 0)
                        edge++;

                    edge1 = Convert.ToInt32(edge);
                    edge2 = edge1;
                    break;
                case GridType.Rect:
                    var tail = cipherLength % 2;
                    if (tail != 0)
                        cipherLength += tail;

                    edge1 = 2;
                    edge2 = cipherLength / 2;
                    if (edge2 % 2 != 0)
                        edge2++;

                    while (edge2 / edge1 > 4)
                    {
                        edge2 /= 2;
                        edge1 *= 2;
                    }

                    if (edge2 == edge1)
                    {
                        edge1 /= 2;
                        edge2 *= 2;
                    }
                    if (edge2 % 2 != 0)
                        edge2++;

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gridType), gridType, null);
            }
            newMessageLength = edge1 * edge2;

            var cardanoGrid = new int[edge1, edge2];
            Random rnd = new();

            for (int a = 0; a < newMessageLength/4; a++)
            {
                var availableRowsNumbersArray =
                    Enumerable.Range(0, edge1)
                        .Where((n) => cardanoGrid.GetRow(n).Contains(0)).ToArray();

                var randomRow = availableRowsNumbersArray[rnd.Next(0, availableRowsNumbersArray.Length - 1)];

                try
                {
                    var currentRow = cardanoGrid.GetRow(randomRow);
                    var possibilityArray = Enumerable.Range(0, edge2 )
                        .Where((n) => currentRow[n] == 0).ToArray();

                    var rndInt = possibilityArray[rnd.Next(0, possibilityArray.Length - 1)];

                    var j = 1;
                    cardanoGrid[randomRow, rndInt] = 1;
                    do
                    {
                        switch (rotationMethod)
                        {
                            case RotationMethod.StraightAngle:
                                if (gridType is GridType.Rect) goto case RotationMethod.OpenAngle;
                                cardanoGrid.Rotate90Clockwise();
                                break;
                            case RotationMethod.OpenAngle:
                                if(j % 2 == 0)
                                    cardanoGrid.ReverseColumns();
                                else
                                    cardanoGrid.ReverseRows();
                                break;
                        }
                        cardanoGrid[randomRow, rndInt] = -1;
                        j++;
                    } while (j < 4);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return new Cipher() {CipherGrid = cardanoGrid, RotationMethod = rotationMethod, GridType = gridType};
        }

        private static int[,] GenerateRectangleGrid(int cipherLength)
        {
            throw new Exception();
        }
        
         public static void CipherFileWithCardano(string filePath, RotationMethod rotationMethod, GridType gridType)
        {
            var fileDirectory = Path.GetDirectoryName(filePath);
            var plainText = ReadTextFromFile(filePath);

            Console.WriteLine(plainText);

            Cipher cipherObj = GenerateCardanoGrid(plainText.Length, rotationMethod, gridType);
            var cipher = cipherObj.CipherGrid;

            cipher.PrintArray();

            var cipherLength = cipher.GetLength(0) * cipher.GetLength(1);

            var sub = cipherLength - plainText.Length;
            if (sub != 0)
               plainText = plainText
                   .PadLeft(plainText.Length + (sub / 2), ' ')
                   .PadRight(plainText.Length + sub, ' ');

            var plainTextQueue = new Queue<char>(plainText.ToArray());

            char [,] cipheredText = new char [cipher.GetLength(0), cipher.GetLength(1)];


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < cipher.GetLength(0); j++)
                {
                    for (int n = 0; n < cipher.GetLength(1); n++)
                    {
                        if (cipher[j, n] == 1)
                            cipheredText[j, n] = plainTextQueue.Dequeue();
                    }
                }

                switch (rotationMethod)
                {
                    case RotationMethod.StraightAngle:
                        if (gridType is GridType.Rect) goto case RotationMethod.OpenAngle;
                        cipher.Rotate90Clockwise();
                        break;
                    case RotationMethod.OpenAngle:
                        if(i % 2 == 0)
                            cipher.ReverseColumns();
                        else
                            cipher.ReverseRows();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(rotationMethod), rotationMethod, null);
                }
            }

            cipheredText.PrintArray();

            string cipheredString = "";

            for (int j = 0; j < cipher.GetLength(0); j++)
            {
                for (int n = 0; n < cipher.GetLength(1); n++)
                {
                    cipheredString += cipheredText[j, n];
                }
            }

            Console.WriteLine(cipheredString);

            WriteCipherKeyToFile(fileDirectory + "key.bin", cipherObj);

            WriteTextToFile(fileDirectory + "ciphertext.txt", cipheredString);

        }

        public static void DecipherTextUsingKey(string keyPath, string cipheredTextPath)
        {
            Cipher cipherObj = ReadCipherKeyFromFile(keyPath);

            var cipher = cipherObj.CipherGrid;

            cipher.PrintArray();

            var cipheredText = ReadTextFromFile(cipheredTextPath);

            var cipheredTextArray = new char[cipher.GetLength(0),cipher.GetLength(1)];

            var cipheredTextQueue = new Queue<char>(cipheredText.ToArray());

            for (int j = 0; j < cipher.GetLength(0); j++)
            {
                for (int n = 0; n < cipher.GetLength(1); n++)
                {
                    cipheredTextArray[j, n] = cipheredTextQueue.Dequeue();
                }
            }

            cipheredTextArray.PrintArray();

            var decipheredString = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < cipher.GetLength(0); j++)
                {
                    for (int n = 0; n < cipher.GetLength(1); n++)
                    {
                        if (cipher[j, n] == 1)
                            decipheredString += cipheredTextArray[j, n];
                    }
                }

                switch (cipherObj.RotationMethod)
                {
                    case RotationMethod.StraightAngle:
                        if (cipherObj.GridType is GridType.Rect) goto case RotationMethod.OpenAngle;
                        cipher.Rotate90Clockwise();
                        break;
                    case RotationMethod.OpenAngle:
                        if(i % 2 == 0)
                            cipher.ReverseColumns();
                        else
                            cipher.ReverseRows();
                        break;
                }
            }

            decipheredString = decipheredString.Trim(' ');
            Console.WriteLine(decipheredString);
            WriteTextToFile( "plainText.txt",decipheredString);
        }

        private static void WriteTextToFile(string filePath, string text)
        {
            FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using StreamWriter bw = new(fileStream);
            bw.Write(text);
        }

        private static void WriteCipherKeyToFile(string filePath, Cipher cipher)
        {
            FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using BinaryWriter bw = new(fileStream);
            BinaryFormatter bf = new();
            bf.Serialize(fileStream, cipher);
        }

        private static string ReadTextFromFile(string filePath)
        {
            FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
            using StreamReader bw = new(fileStream);
            return bw.ReadToEnd();
        }

        private static Cipher ReadCipherKeyFromFile(string filePath)
        {
            FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
            using BinaryReader bw = new(fileStream);
            BinaryFormatter bf = new();
            Cipher cipher = bf.Deserialize(fileStream) as Cipher;
            return cipher;
        }
    }
}