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

namespace RSAWPF
{
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


        public BigRSA rsa;

        public string src;
        public string dest;

        public int approxLenght=1024;
        public void showstats()
        {
            MessageBox.Show("p= " + rsa.P.ToString() + "\n" +
                "q= " + rsa.Q.ToString() + "\n" +
                "n= " + rsa.N.ToString() + "\n" +
                "phi= " + rsa.PHI.ToString() + "\n" +
                "e= " + rsa.E.ToString() + "\n" +
                "d= " + rsa.D.ToString() + "\n" +
                "d*e%phi= " + ((rsa.D * rsa.E) % rsa.PHI).ToString()+
                "GCD(e,phi)= "+rsa.GCD(rsa.E,rsa.PHI));

        }

        public void MainWindowLoad()
        {
           
        }

        private void GetSrcFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "TXT Files (*.txt)|*.txt";
            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                src = openFileDialog1.FileName;
                SourceFileBox.Text = src;
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
                DestinationFileBox.Text = dest;
            }
        }

        private async void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Encrypting...");
            StreamWriter sw = new StreamWriter(dest);
            if(pInput.Text=="")  { rsa = await Task.Run(() => ComputeRSA());/*new BigRSA(approxLenght)*/;}
            else{ rsa = new BigRSA(BigInteger.Parse(pInput.Text), BigInteger.Parse(qInput.Text), BigInteger.Parse(eInput.Text));}
            string buffer = File.ReadAllText(src);
            char[] b = buffer.ToCharArray();
            BigInteger a;
            for (int i = 0; i < b.Length-1; i++)
            {
                a = b[i];
                a = BigInteger.ModPow(a, rsa.E, rsa.N);
                sw.Write(a + "-");
            }
            a = b[b.Length-1];
            a= BigInteger.ModPow(a, rsa.E,rsa.N);
            sw.Write(a);
            sw.Flush();
            sw.Close();
            MessageBox.Show("Encrypted");
        }

        private async void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Decrypting...");
            if (pInput.Text == "") {  rsa = rsa = await Task.Run(() => ComputeRSA()); /*new BigRSA(approxLenght)*/; }
            else{ rsa = new BigRSA(BigInteger.Parse(pInput.Text), BigInteger.Parse(qInput.Text), BigInteger.Parse(eInput.Text));}
            string buffer = File.ReadAllText(src);
            StreamWriter sw = new StreamWriter(dest);
            string[] c = buffer.Split('-');
            for(int i=0;i<c.Length;i++)
            {
                BigInteger a = BigInteger.Parse(c[i]);
                a = BigInteger.ModPow(a, rsa.D, rsa.N);
                int ax = (int)a;
                sw.Write((char)ax);
            }
            sw.Flush();
            sw.Close();
            MessageBox.Show("Decrypted");
        }

        private BigRSA ComputeRSA()
        {
            return new BigRSA(approxLenght);
        }

        private void ShowStats_Click(object sender, RoutedEventArgs e)
        {
            showstats();
        }

        private void UseCurrentStats_Click(object sender, RoutedEventArgs e)
        {
            pInput.Text = rsa.P.ToString();
            qInput.Text = rsa.Q.ToString();
            eInput.Text = rsa.E.ToString();
        }
    }
}

