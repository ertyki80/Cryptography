using System;

namespace Cryptography.Bll.Implementation
{
    public static class CardanoCipher
    {
        private readonly static int[] holePosition = new int[4];
        private readonly  static char[] arr_en =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '.', ',', '-', '!', '?', ' '
        };
        
        public static void  Initialize(string key)
        {
            for (int i = 0; i < holePosition.Length; i++)
            {
                holePosition[i] = GetHolePosition(key[i]);
            }
        }
         
        private static int GetHolePosition(char keyPart) => keyPart switch
        {
            '1' => 3,
            '2' => 2,
            '4' => 1,
            '8' => 0,
            _ => -1
        };
        
        public static string Decode(string plainText)
        {
            string decoded = "";
            char[,] charMatr = new char[4, 4];
            int temp = 0;
            while (plainText.Length > temp)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (plainText.Length > temp)
                        {
                            charMatr[i, j] = plainText[temp];
                            temp += 1;
                        }
                        else
                        {
                            charMatr[i, j] = '\0';
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (holePosition[j] != -1)
                        {
                            decoded += charMatr[j, holePosition[j]];
                        }
                    }
                    charMatr = Rotation(charMatr);
                }
            }
            return decoded;
        }
        
        public static string Encode(string plainText)
        {
            string encoded = "";
            char[,] charMatr = new char[4, 4];
            Random random = new();

            int temp = 0;
            while (plainText.Length > temp)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (holePosition[j] != -1 && plainText.Length > temp)
                        {
                            charMatr[j, holePosition[j]] = plainText[temp];
                            temp += 1;
                        }
                    }
                    charMatr = Rotation(charMatr);
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (charMatr[i, j] == '\0')
                        {
                            encoded += arr_en[random.Next() % arr_en.Length];
                        }
                        else
                        {
                            encoded += charMatr[i, j];
                        }
                    }
                }
                charMatr = new char[4, 4];
            }
            return encoded;
        }

        public static string BruteForce(string plainText,string resultMustBe)
        {
            string decodedText = "";
            string data = resultMustBe;
            string result = "";
            result += "We know that text start with:\n____________________\n" + data + "\n____________________";
            int[] holePosition = new int[4];
            char[,] charMatr = new char[4, 4];
            string key = "";

            int temp = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (plainText.Length > temp)
                    {
                        charMatr[i, j] = plainText[temp];
                        temp += 1;
                    }
                    else
                    {
                        charMatr[i, j] = '\0';
                    }
                }
            }

            temp = 0;
            bool zeroRow;
            for (int i = 0; i < 4; i++)
            {
                zeroRow = true;
                for (int j = 0; j < 4; j++)
                {
                    if(charMatr[i,j] == data[temp])
                    {
                        key += GetHolePositionKey(j);
                        temp += 1;
                        zeroRow = false;
                        break;
                    }
                }
                if (zeroRow)
                {
                    key += GetHolePositionKey(-1);
                }
            }

            result +="\nFinded key : " + key;
            return result;
        }
        
        private static char GetHolePositionKey(int position) => position switch
        {
            3 => '1',
            2 => '2',
            1 => '4',
            0 => '8',
            _ => '0'
        };
        
        private static char[,] Rotation(char[,] matr)
        {
            var result = new char[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = matr[4 - j - 1, i];
                }
            }
            return result;
        }

    }
}