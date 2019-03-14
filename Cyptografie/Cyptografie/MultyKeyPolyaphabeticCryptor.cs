using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cyptografie
{
    class MultiKeyPolyaphabeticCryptor:Cryptography
    {
        List<Dictionary<char, char>> convertor = new List<Dictionary<char, char>>();
        public override string message { get; set; }
        public List<char[]> keyst = new List<char[]>();
        static Random rnd = new Random();
        public MultiKeyPolyaphabeticCryptor(string message, List<char[]> keys, int k)
        {
            this.message = message;
            this.message = message.ToLower();
            for (int j = 0; j < keys.Count; j++)
            {
                convertor.Add(new Dictionary<char, char>());
                for (int i = 0; i < 26; i++)
                {
                    Console.WriteLine(keys[j][i]);
                    convertor[j].Add(keys[j][i], (char)('a' + i));
                }
            }
            DeCrypt(k);

        }

        public override void ReadFromFile(string FileName)
        {
            message = File.ReadAllText(@"../../" + FileName + ".txt");
        }

        public override void WriteToFile(string FileName)
        {
            File.WriteAllText(@"../../" + FileName + ".txt", message);
        }

        public MultiKeyPolyaphabeticCryptor(string message, int KeyCount, int CrytpingCount)
        {
            this.message = message;
            Cryptor(KeyCount, CrytpingCount);
        }

        public void DeCrypt(int k)
        {
            message = message.ToLower();
            char[] MessageInLetters = message.ToCharArray();
            int nr = 0;
            for (int i = 1; i <= MessageInLetters.Length; i++)
            {
                if (Char.IsLetter(MessageInLetters[i - 1]))
                {
                    MessageInLetters[i - 1] = convertor[nr][MessageInLetters[i - 1]];
                    if (i % k == 0)
                    {
                        nr++;
                        if (nr % convertor.Count == 0)
                        {
                            nr = 0;
                        }
                    }
                }
            }
            message = new string(MessageInLetters);
        }

        public void Cryptor(int KeyCount, int CryptingCont)
        {
            List<char[]> keyList = new List<char[]>();
            for (int i = 0; i < KeyCount; i++)
            {
                keyList.Add(new char[26]);
                for (int j = 0; j < 26; j++)
                {
                    keyList[i][j] = (char)('a' + j);
                }
                for (int j = 0; j < 26; j++)
                {
                    int n = rnd.Next(26 - j);
                    char aux;
                    aux = keyList[i][j];
                    keyList[i][j] = keyList[i][n];
                    keyList[i][n] = aux;
                }
            }
            int nr = 0;
            message = message.ToLower();
            char[] MessageInLetters = message.ToCharArray();
            for (int i = 1; i <= MessageInLetters.Length; i++)
            {
                if (Char.IsLetter(MessageInLetters[i - 1]))
                {
                    MessageInLetters[i - 1] = keyList[nr][(char)(MessageInLetters[i - 1] - 'a')];
                    if (i % CryptingCont == 0)
                    {
                        nr++;
                        if (nr % KeyCount == 0)
                        {
                            nr = 0;
                        }
                    }
                }
            }
            message = new string(MessageInLetters);
            keyst = keyList;
        }

        public override void ShowMessageOnScreen()
        {
            Console.WriteLine(message);
        }
    }
}

