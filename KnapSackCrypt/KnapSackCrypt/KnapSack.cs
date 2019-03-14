using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;

namespace KnapSackCrypt
{
    public class KnapSack
    {
        private static Random rnd=new Random();
        public int[] BinarySequence;
        public int[] SuperIncreasingSequence;
        public int[] Perm;
        public int[] publicKey;

        //public string crypted;
        //public string decrypted;

        int sum=0;
        public int M;
        public int W;

        public KnapSack(int lenghtOfSequence,int increaseRate)
        {
            GenerateSuperincreasingSequence(lenghtOfSequence, increaseRate);
            CalculateM(increaseRate);
            GenerateRandomW();
            CreatePerm();
            CreatePublicKey();
        }

        public KnapSack(int [] SuperIncreasingSequence, int[] Perm,int M,int W)
        {
            this.SuperIncreasingSequence = SuperIncreasingSequence;
            this.Perm = Perm;
            this.M = M;
            this.W = W;
            CreatePublicKey();
        }

        public string Encrypt(string g)
        {
            string result="";
            char[] v = g.ToCharArray();
            for(int i=0;i<v.Length;i++)
            {
                string a = Convert.ToString((int)v[i], 2);
                List<int> e = new List<int>();
                char[] b = a.ToCharArray();
                for(int j=0;j<a.Length;j++)
                {
                    if (a[j] == '0') { e.Add(0); }
                    else { e.Add(1); }
                }
                e.Reverse();
                for(int j=0;j<Perm.Length-b.Length;j++)
                {
                    e.Add(0);
                }
                e.Reverse();

                int res = 0;
                for(int j=0;j<e.Count;j++)
                {
                    res += e[j] * publicKey[j];
                }
                result += res.ToString();
                if(i!=v.Length-1)
                {
                    result += " ";
                }
            }
            return result;
        }

        public string Decrypt(string g)
        {
            string y = "";
            string[] q = g.Split(' ');
            for (int i = 0; i < q.Length; i++)
            {
                int l = 1;
                while(true)
                {
                    if(W*l%M==1)
                    {
                        break;
                    }
                    l++;
                }
                int a = (int.Parse(q[i]) * l) % M;

                int[] v = SolveSuperincreasingSequence(SuperIncreasingSequence, a);

                List<int> res = new List<int>();
                string h = "";
                for (int j = 0; j < v.Length; j++)
                {
                    res.Add(v[Perm[j]]);
                    h += res[j].ToString();
                }
                y += h;
                if (i != q.Length - 1)
                {
                    y += " ";
                }
            }
            //int b = Convert.ToInt32(y, 2);
            string[] aux = y.Split(' ');
            y = "";
            for(int i=0;i<aux.Length;i++)
            {
                y +=(char)Convert.ToInt32(aux[i],2);
                //if(i!=aux.Length-1)
                //{
                //    y += " ";
                //}

            }
             return y;
        }

        public void CreatePublicKey()
        {
            publicKey = new int[SuperIncreasingSequence.Length];
            for(int i=0;i<publicKey.Length;i++)
            {
                publicKey[i] = (W*SuperIncreasingSequence[Perm[i]]) % M;
            }
        }

        public void CreatePerm()
        {
            Perm = new int[SuperIncreasingSequence.Length];
            for(int i=0;i<SuperIncreasingSequence.Length;i++)
            {
                Perm[i] = i;
            }
            for(int i=0;i<Perm.Length;i++)
            {
                int aux = Perm[i];
                int r = rnd.Next(i, Perm.Length);
                Perm[i] = Perm[r];
                Perm[r] = aux;
            }
        }

        public void CalculateM(int increase)
        {
           for(int i=0;i<SuperIncreasingSequence.Length;i++)
           {
                sum += SuperIncreasingSequence[i];
           }
            sum += rnd.Next(1,increase);
            M = sum;
        }

        public int GCD(int e, int phi)
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

        public void GenerateRandomW()
        {
            int aux;
            List<int> Exists = new List<int>();
            while (true)
            {
                aux = rnd.Next(2, M);
                while (Exists.Contains(aux))
                {
                    aux = rnd.Next(2, M);
                }
                if (GCD(aux, M) == 1)
                {
                    break;
                }
                Exists.Add(aux);
            }
            W = aux;
        }

        public void GenerateSuperincreasingSequence(int numberOfElements,int increaseRate)
        {
            int[] v = new int[numberOfElements];
            int currentSum=0;
            for(int i=0;i<2;i++)
            {
                if(i<v.Length)
                {
                    v[i] = i + 1;
                    currentSum += i + 1;
                }
            }
            for(int i=2;i<numberOfElements;i++)
            {
                int aux= rnd.Next(1, increaseRate);
                v[i] = currentSum + aux;
                currentSum += aux+currentSum;
            }
            SuperIncreasingSequence = v;
        }

        public static int GetRandomSum(int [] v)
        {
            int numbersOfSum = rnd.Next(1, v.Length);
            List<int> elements = new List<int>();
            elements = v.ToList<int>();
            int sum = 0;
            for(int i=0;i<numbersOfSum;i++)
            {
                int aux = elements[rnd.Next(elements.Count)];
                elements.Remove(aux);
                sum += aux;
            }
            return sum;
        }

        //public void SolveSuperincreasingSequence(int [] SuperincreasingSequence, int Sum)
        //{
        //    BinarySequence = new int[SuperincreasingSequence.Length];
        //    int i = SuperincreasingSequence.Length-1;
        //    while (i >= 0)
        //    {
        //        if (Sum >= SuperincreasingSequence[i])
        //        {
        //            BinarySequence[i] = 1;
        //            Sum = Sum - SuperincreasingSequence[i];
        //        }
        //        else
        //        {
        //            BinarySequence[i] = 0;
        //        }
        //        i--;
        //    }
        //}

        //public int [] SolveSuperincreasingSequenceasd(int[] SuperincreasingSequence, int Sum)
        //{
        //    int [] BinarySequenceasd = new int[SuperincreasingSequence.Length];
        //    int i = SuperincreasingSequence.Length - 1;
        //    while (i >= 0)
        //    {
        //        if (Sum >= SuperincreasingSequence[i])
        //        {
        //            BinarySequenceasd[i] = 1;
        //            Sum = Sum - SuperincreasingSequence[i];
        //        }
        //        else
        //        {
        //            BinarySequenceasd[i] = 0;
        //        }
        //        i--;
        //    }
        //    return BinarySequenceasd;
        //}

        public int[] SolveSuperincreasingSequence(int[] SuperincreasingSequence, int Sum)
        {
            int[] BinarySequenceasd = new int[SuperincreasingSequence.Length];
            int i = SuperincreasingSequence.Length - 1;
            while (i >= 0)
            {
                if (Sum >= SuperincreasingSequence[i])
                {
                    BinarySequenceasd[i] = 1;
                    Sum = Sum - SuperincreasingSequence[i];
                }
                else
                {
                    BinarySequenceasd[i] = 0;
                }
                i--;
            }
            return BinarySequenceasd;
        }
    }
}
