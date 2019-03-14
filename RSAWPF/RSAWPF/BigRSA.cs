using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
using System.IO;
using System.Windows;

namespace RSAWPF
{
    public class BigRSA
    {
        public static Random rnd = new Random();

        BigInteger p = 0, q = 0;
        BigInteger n, phi;
        BigInteger e, d;

        public BigInteger P
        {
            get { return p; }
        }
        public BigInteger Q
        {
            get { return q; }
        }
        public BigInteger N
        {
            get { return n; }
        }
        public BigInteger PHI
        {
            get { return phi; }
        }
        public BigInteger E
        {
            get { return e; }
        }
        public BigInteger D
        {
            get { return d; }
        }

        public BigRSA(BigInteger p, BigInteger q, BigInteger e)
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


        public BigRSA(int approxLenght) { ProcessRSA(approxLenght); }


        public void ProcessRSA(int aproxLenght)
        {
            BigInteger a = BigInteger.Pow(2, aproxLenght);
            GenerateTwoRandomPrimes(a);
            n = p * q;
            phi = (p - 1) * (q - 1);
            GenerateRandomE();
            d = gcdExtended();
            if (d > phi)
            {
                d = d - phi;
            }
        }


        public static bool MillerRabinTest(BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }


            for (int i = 0; i < certainty; i++)
            {
                BigInteger a = GetRandomBigInt(2, source-2);
                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }


        public static BigInteger Exponentiere(BigInteger Number, BigInteger Exp)
        {
            if (Exp == 0) return 1;
            if (Exp == 1) return Number;
            if (Exp % 2 == 0)
            {
                return Exponentiere(Number*Number, Exp / 2);
            }
            else
            {
                return Number * Exponentiere(Number * Number, (Exp - 1) / 2);
            }
        }


        public static BigInteger GetRandomBigInt(BigInteger minVal,BigInteger maxVal)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[maxVal.ToByteArray().LongLength];
            BigInteger a;
            do
            {
                rng.GetBytes(bytes);
                a = new BigInteger(bytes);
            }
            while (a < minVal || a >= maxVal);
            return a;
        }

        public void GenerateTwoRandomPrimes(BigInteger source)
        {
            List<BigInteger> Exists = new List<BigInteger>();
            BigInteger a;
            while (true)
            {
                a = BigRSA.GetRandomBigInt(2, source - 2);
                while (Exists.Contains(a))
                {
                    a = BigRSA.GetRandomBigInt(2, source - 2);
                }
                if (MillerRabinTest(a, 5))
                {
                    break;
                }
                Exists.Add(a);
            }
            p = a;
            Exists.Add(p);
            while (true)
            {
                a = BigRSA.GetRandomBigInt(2, source - 2);
                while (Exists.Contains(a))
                {
                    a = BigRSA.GetRandomBigInt(2, source - 2);
                }
                if (MillerRabinTest(a, 5))
                {
                    break;
                }
                Exists.Add(a);
            }
            q = a;
        }

        public BigInteger GCD(BigInteger e, BigInteger phi)
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
            if (e == 0)
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
            List<BigInteger> Exists = new List<BigInteger>();
            BigInteger aux;
            while (true)
            {
                aux = BigRSA.GetRandomBigInt(2, phi);
                while (Exists.Contains(aux))
                {
                    aux = BigRSA.GetRandomBigInt(2, phi);
                }
                if (GCD(aux, phi) == 1)
                {
                    break;
                }
                Exists.Add(aux);
            }
            e = aux;
        }


        public BigInteger gcdExtended()
        {
            BigInteger a = e;
            BigInteger m = phi;
            BigInteger b = phi;
            BigInteger x = 0;
            BigInteger y = 1;
            while (true)
            {
                if (a == 1) { return y; }
                if (a == 0) { return b; }
                BigInteger q = b / a;
                b = b - a * q;
                x = x + q * y;
                if (b == 1) { return m - x; }
                if (b == 0) { return a; }
                q = a / b;
                a = a - b * q;
                y = y + q * x;
            }
        }
    }
}
