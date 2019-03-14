using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSAWPF
{
    public class RSA
    {
        public static Random rnd=new Random();

        int p=0, q=0;
        int n, phi;
        int e,d;

        public int P
        {
            get { return p; }
        }
        public int Q
        {
            get { return q; }
        }
        public int N
        {
            get { return n; }
        }
        public int PHI
        {
            get { return phi; }
        }
        public int E
        {
            get { return e; }
        }
        public int D
        {
            get { return d; }
        }

        public RSA(int p,int q,int e)
        {
            this.q = q;
            this.p = p;
            this.e = e;
            n = p * q;
            phi = (p - 1) * (q - 1);
            d = gcdExtended();
            if (d > phi)
            {
                d = d - phi;
            }
        }

        public RSA() { }

        public void ProcessRSA()
        {
            GenerateTwoRandomPrimes();
            n = p * q;
            phi = (p - 1) * (q - 1);
            GenerateRandomE();
            d = gcdExtended();
            if(d>phi)
            {
                d = d - phi;
            }
        }

        public static bool MillerRabinTest(int n , int k)
        {
            if ((n < 2) || (n % 2 == 0)) return (n == 2);

            int s = n - 1;
            while (s % 2 == 0) s >>= 1;

            Random r = new Random();
            for (int i = 0; i < k; i++)
            {
                int a = r.Next(n - 1) + 1;
                int temp = s;
                long mod = 1;
                for (int j = 0; j < temp; j++) mod = (mod * a) % n;
                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp *= 2;
                }

                if (mod != n - 1 && temp % 2 == 0) return false;
            }
            return true;
        }

        public static bool FermatTest(int Number,int TestsRun)
        {
            if (Number < 2 || Number % 2 == 0) return false;
            for(int i=0;i<TestsRun;i++)
            {
                int u = rnd.Next(2, Number - 1);
                if(Exponentiere(u,Number-1)%Number!=1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool NaivePrimeTest(int Number)
        {
            if (Number == 2) { return true; }
            if(Number<2||Number%2==0)
            {
                return false;
            }
            for(int i=3;i<=Math.Sqrt(Number);i+=2)
            {
                if(Number%i==0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int Exponentiere(int Number,int Exp)
        {
            if (Exp == 0) return 1;
            if (Exp == 1) return Number;
            if(Exp%2==0)
            {
                return Exponentiere(Number, Exp / 2)*Exponentiere(Number,Exp/2);
            }
            else
            {
                return Number * Exponentiere(Number, Exp - 1);
            }
        }

        public void GenerateTwoRandomPrimes()
        {
            List<int> Exists = new List<int>();
            int aux;
            while (true)
            {
                aux = rnd.Next(4, 8);
                while (Exists.Contains(aux))
                {
                    aux = rnd.Next(4, 8);
                }
                if(MillerRabinTest(aux,5))
                {
                    break;
                }
                Exists.Add(aux);
            }
            p = aux;
            Exists.Add(p);
            while (true)
            {
                aux = rnd.Next(4, 8);
                while (Exists.Contains(aux))
                {
                    aux = rnd.Next(4, 8);
                }
                if (MillerRabinTest(aux, 5))
                {
                    break;
                }
                Exists.Add(aux);
            }
            q = aux;
        }

        public int GCD(int e,int phi)
        {
            while (e != 0 && phi != 0)
            {
                if (e > phi)
                {
                    e %= phi;
                }
                else
                {
                    phi %= e;
                }
            }
            if(e==0)
            {
                return phi;
            }
            else
            {
                return e;
            }
        }

        public void GenerateRandomE()
        {
            int aux;
            List<int> Exists = new List<int>();
            while (true)
            {
                aux = rnd.Next(2, phi);
                while (Exists.Contains(aux))
                {
                    aux = rnd.Next(2, phi);
                }
                if (GCD(aux,phi)==1)
                {
                    break;
                }
                Exists.Add(aux);
            }
            e = aux;
        }

        public void CalculateD()
        {
            int aux = 2;
            while(true)
            {
                if(aux*e%phi==1)
                {
                    break;
                }
                else { aux++; }
            }
            if(d>phi)
            {
                d = d - phi;
            }
            d = aux;
        }

        public int gcdExtended()
        {
            int a = e;
            int m = phi;
            int b = phi;
            int x = 0;
            int y = 1;
            while (true)
            {
                if (a == 1) { return y; }
                if (a == 0) { return b; }
                int q = Math.Abs(b / a);
                b = b - a * q;
                x = x + q * y;
                if (b == 1) { return m - x; }
                if (b == 0) { return a; }
                q = Math.Abs(a / b);
                a = a - b * q;
                y = y + q * x;
            }
        }
    }
}
