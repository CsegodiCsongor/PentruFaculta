using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cyptografie
{
    public class PolylAlphabeticCrypt:Cryptography
    {
        public char[] letters;
        static Random rnd = new Random();
        Dictionary<char, char> convertor = new Dictionary<char, char>();
        char[] key;
        char[] MostFrequentLetters;
        public override string message { get; set; }

        public PolylAlphabeticCrypt(string message)
        {
            letters = new char[26];
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = (char)(i + 'a');
            }
        }

        public PolylAlphabeticCrypt(string message, char[] key)
        {
            this.message = message;
            this.key = key;
            FillDic();
        }

        void Shuffle()
        {
            for (int i = letters.Length - 1; i >= 0; i--)
            {
                char aux = letters[i];
                int t = rnd.Next(25 - i);
                letters[i] = letters[t];
                letters[t] = aux;
            }
        }

        public override void ReadFromFile(string FileName)
        {
            message = File.ReadAllText(@"../../" + FileName + ".txt");
        }

        public void Encrypt()
        {
            message = message.ToLower();
            char[] CryptedMessage = message.ToCharArray();
            for (int i = 0; i < CryptedMessage.Length; i++)
            {
                if (Char.IsLower(CryptedMessage[i]))
                {
                    CryptedMessage[i] = letters[CryptedMessage[i] - 'a'];
                }
            }
            message = new string(CryptedMessage);
            WriteToFile("RandomCrypted");
        }

        public void PolyalphabeticDeCryptoAnalysis(string message,string FileWithLetters)
        {
            this.message = message;
            MostFrequentLetters = new char[26];
            string[] letter = File.ReadAllText(@"../../" + FileWithLetters + ".txt").Split(' ');
            for(int i=0;i<26;i++)
            {
                MostFrequentLetters[i] = char.Parse(letter[i]);
            }
            char[] letters = new char[26];
            for (int i = 0; i < 26; i++)
            {
                letters[i] = (char)('a' + i);
            }
            int[] count = new int[26];
            message = message.ToLower();
            for (int i = 0; i < message.Length; i++)
            {
                if (Char.IsLetter(message[i]))
                {
                    count[(char)(message[i]) - 'a']++;
                }
            }
            for (int i = 0; i < 25; i++)
            {
                for (int j = i + 1; j < 26; j++)
                {
                    if (count[i] < count[j])
                    {
                        char auxc = letters[i];
                        letters[i] = letters[j];
                        letters[j] = auxc;
                        int aux = count[i];
                        count[i] = count[j];
                        count[j] = aux;
                    }
                }
            }
            for (int i = 0; i < 26; i++)
            {
                Console.WriteLine(letters[i]);
            }
            for (int i = 0; i < 26; i++)
            {
                convertor.Add(letters[i], MostFrequentLetters[i]);
            }
            Decrypt();
        }

        void FillDic()
        {
            for (int i = 0; i < key.Length; i++)
            {
                convertor.Add(key[i], (char)('a' + i));
            }
        }

        public void Decrypt()
        {
            message = message.ToLower();
            char[] MessageInLetters = message.ToCharArray();
            for (int i = 0; i < MessageInLetters.Length; i++)
            {
                if (Char.IsLetter(MessageInLetters[i]))
                {
                    MessageInLetters[i] = convertor[MessageInLetters[i]];
                }
            }
            message = new string(MessageInLetters);

        }

        public override void ShowMessageOnScreen()
        {
            Console.WriteLine(message);
        }

        public override void WriteToFile(string FileDestName)
        {
            File.WriteAllText(@"../../" + FileDestName + ".txt", message);
        }
    }
}
