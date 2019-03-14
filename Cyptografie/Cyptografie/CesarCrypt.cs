using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cyptografie
{
    public class CesarCrypt:Cryptography
    {
        public override string message { get; set; }

        public override void ReadFromFile(string FileName)
        {
            message = File.ReadAllText(@"../../" + FileName + ".txt");
        }

        public void EnCrypt(int n)
        {
            message = message.ToLower();
            char[] MessageInLetters = message.ToCharArray();
            for (int i = 0; i < MessageInLetters.Length; i++)
            {
                if (Char.IsLetter(MessageInLetters[i]))
                {
                    MessageInLetters[i] = (char)(MessageInLetters[i] + n % 26);
                    if (MessageInLetters[i] > (int)'z')
                    {
                        MessageInLetters[i] = (char)(MessageInLetters[i] - 26);
                    }
                }
            }
            message = new string(MessageInLetters);
        }

        public void DeCrypt(int n)
        {
            message = message.ToLower();
            char[] MessageInLetters = message.ToCharArray();
            for (int i = 0; i < MessageInLetters.Length; i++)
            {
                if (Char.IsLetter(MessageInLetters[i]))
                {
                    MessageInLetters[i] = (char)(MessageInLetters[i] - n % 26);
                    if (MessageInLetters[i] < (int)'a')
                    {
                        MessageInLetters[i] = (char)(MessageInLetters[i] + 26);
                    }
                }
            }
            message = new string(MessageInLetters);
        }

        public void FindDecryption(string FileDestionationName, string FileWithWords)
        {
            string words = File.ReadAllText(@"../../" + FileWithWords + ".txt");
            string[] WordsArray = File.ReadAllLines(@"../../" + FileWithWords + ".txt");
            Console.WriteLine();
            int count = 0;
            int countm = 0;
            int maxC = 0;
            for (int i = 1; i < 26; i++)
            {
                DeCrypt(1);
                string[] ml = message.Split(' ');
                for (int q = 0; q < WordsArray.Length; q++)
                {
                    for (int w = 0; w < ml.Length; w++)
                    {
                        if (ml[w] == WordsArray[q])
                        {
                            count++;
                        }
                    }
                }
                if (count > countm)
                {
                    maxC = i;
                    countm = count;
                }
                count = 0;
            }
            Console.WriteLine(maxC);
            Console.WriteLine(message);
            DeCrypt(maxC + 1);
            Console.WriteLine(message);
            message = "n=" + maxC + " " + message;
        }

        public override void ShowMessageOnScreen()
        {
            Console.WriteLine(message);
        }

        public override void WriteToFile(string FileDestinationName)
        {
            File.WriteAllText(@"../../" + FileDestinationName + ".txt", message);
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
