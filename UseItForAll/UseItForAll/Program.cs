using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace UseItForAll
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stack<int> s = new Stack<int>();
            //for(int i=0;i<10;i++)
            //{
            //    s.AddBegining(i);
            //}
            //Console.WriteLine(s.ToString());
            //s.Sort(delegate (int a, int b){
            //    return a.CompareTo(b);
            //});

            //Console.WriteLine(s.ToString());

            //Console.WriteLine(s.Count);

            //int[] v = new int []{ 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            //int[] v = new int[] { 4, 3, 2, 10, 12, 1, 5, 6};
            //Sorts<int>.BubbleSort(v);
            //for(int i=0;i<v.Length;i++)
            //{
            //    Console.Write(v[i].ToString() + " ");
            //}

            byte[] bytes = new byte[] { 1, 2, 3 };
            string g = "123";
            BigInteger b = new BigInteger(ASCIIEncoding.ASCII.GetBytes(g));
            Console.WriteLine(b.ToString());
        }
    }
}
