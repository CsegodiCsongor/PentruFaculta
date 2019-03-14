using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
using System.IO;

namespace KnapSackCrypt
{

    public enum CryptMode
    {
        Encrypt,Decrypt
    };
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowLoad();
        }

        public void Test()
        {
            //KnapSack t = new KnapSack();
            //t.GenerateSuperincreasingSequence(10, 10);
            //int s = KnapSack.GetRandomSum(t.SuperIncreasingSequence);
            //t.SolveSuperincreasingSequence(t.SuperIncreasingSequence, s);
            //for (int i = 0; i < t.SuperIncreasingSequence.Length; i++)
            //{
            //    SrcFileBox.Text += t.SuperIncreasingSequence[i].ToString() + " ";
            //}
            //SrcFileBox.Text += "\n";
            //SrcFileBox.Text += s.ToString() + "\n";
            //for (int i = 0; i < t.BinarySequence.Length; i++)
            //{
            //    SrcFileBox.Text += t.BinarySequence[i].ToString();
            //}
            //SrcFileBox.Text += "\n";
            //t.CalculateM(10);
            //SrcFileBox.Text += "M= " + t.M + "\n";
            //t.GenerateRandomW();
            //SrcFileBox.Text += "W= " + t.W + "\n";
            //SrcFileBox.Text += t.GCD(t.M, t.W).ToString() + "\n";
            //t.CreatePerm();
            //t.CreatePublicKey();
            //for (int i = 0; i < t.Perm.Length; i++)
            //{
            //    SrcFileBox.Text += t.Perm[i].ToString() + " ";
            //}
            //SrcFileBox.Text += "\n";
            //for (int i = 0; i < t.publicKey.Length; i++)
            //{
            //    SrcFileBox.Text += t.publicKey[i].ToString() + " ";
            //}

            //string q = "A B C DFG";
            //t.Encrypt(q);
            //t.Decrypt(t.crypted);
            //MessageBox.Show(t.crypted);
            //MessageBox.Show(t.decrypted);
        }

        public void MainWindowLoad()
        {
            //Test();
        }

        string src;
        string dest;
        KnapSack knap;

        private void GetSrcFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                src = openFileDialog1.FileName;
                SrcFileBox.Text = src;
            }
        }

        private void GetDestFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                dest = openFileDialog1.FileName;
                DestFileBox.Text = dest;
            }
        }

        public bool CheckEmpty()
        {
            if(SuperIncrBox.Text!=""&&PermBox.Text!=""&&WBox.Text!=""&&MBox.Text!="")
            {
                return false;
            }
            return true;
        }

        public void Crypt(CryptMode mode)
        {
            string msg = File.ReadAllText(src);
            if (CheckEmpty() == true)
            {
                knap = new KnapSack(10,10);
            }
            else
            {
                char[] forb = { ' ', ',' };
                string[] su = SuperIncrBox.Text.Split(forb, StringSplitOptions.RemoveEmptyEntries);
                string[] per = PermBox.Text.Split(forb, StringSplitOptions.RemoveEmptyEntries);
                int M = int.Parse(MBox.Text);
                int W = int.Parse(WBox.Text);
                int[] sui = new int[su.Length];
                int[] peri = new int[per.Length];
                for (int i = 0; i < sui.Length; i++)
                {
                    sui[i] = int.Parse(su[i]);
                    peri[i] = int.Parse(per[i]);
                }
                knap = new KnapSack(sui, peri, M, W);
            }

            if (mode == CryptMode.Encrypt)
            {
                msg = knap.Encrypt(msg);
            }
            else
            {
                msg = knap.Decrypt(msg);
            }
            File.WriteAllText(dest, msg);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            Crypt(CryptMode.Encrypt);
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            Crypt(CryptMode.Decrypt);
        }

        private void UseCurStats_Click(object sender, RoutedEventArgs e)
        {
            for(int i=0;i<knap.Perm.Length;i++)
            {
                PermBox.Text += knap.Perm[i].ToString()+" ";
                SuperIncrBox.Text += knap.SuperIncreasingSequence[i].ToString() + " ";
            }
            MBox.Text = knap.M.ToString();
            WBox.Text = knap.W.ToString();
        }
    }
}
