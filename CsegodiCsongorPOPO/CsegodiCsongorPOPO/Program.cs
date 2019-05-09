using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CsegodiCsongorPOPO
{
    class Program
    {
        static StreamReader f;
        static StreamWriter w;
        static Companie<Angajat> comp = new Companie<Angajat>();

        static void Citire()
        {
            string buffer;
            while((buffer=f.ReadLine())!=null)
            {
                string[] v = buffer.Split(' ');
                string[] v1 = v[2].Split('/',',','.');
                comp.Add(new Angajat(v[0], v[1], new DateTime(int.Parse(v1[0]), int.Parse(v1[1]), int.Parse(v1[2]))));
            }
        }

        static void SortByDate()
        {
            for (int i = 0; i < comp.GetLength(); i++)
            {
                for (int j = i; j < comp.GetLength(); j++)
                {
                    if (comp.GetAngajat(i).GetDate().Year > comp.GetAngajat(j).GetDate().Year)
                    {
                        comp.ChangeAngajat(i, j);

                    }
                    else if (comp.GetAngajat(i).GetDate().Month > comp.GetAngajat(j).GetDate().Month &&
                        comp.GetAngajat(i).GetDate().Year == comp.GetAngajat(j).GetDate().Year)
                    {
                        comp.ChangeAngajat(i, j);
                    }
                    else if (comp.GetAngajat(i).GetDate().Month == comp.GetAngajat(j).GetDate().Month &&
                        comp.GetAngajat(i).GetDate().Year == comp.GetAngajat(j).GetDate().Year &&
                        comp.GetAngajat(i).GetDate().Day > comp.GetAngajat(j).GetDate().Day)
                    {
                        comp.ChangeAngajat(i, j);
                    }
                }
            }
        }

        //static void SortByName()
        //{
        //    for (int i = 0; i < comp.GetLength(); i++)
        //    {
        //        for (int j = i; j < comp.GetLength(); j++)
        //        {
        //            if(comp.GetAngajat(i).CompareTo(comp.GetAngajat(j))>0)
        //            {
        //                comp.ChangeAngajat(i, j);
        //            }
        //        }
        //    }
        //}


        //static void test()
        //{
        //    comp.Add(new Angajat("aaa", "bas", new DateTime(1992, 7, 12)));
        //    for (int i = 0; i < comp.GetLength(); i++)
        //    {
        //        Console.WriteLine(comp.GetAngajat(i));
        //    }
        //    Console.WriteLine();
        //    comp.RemoveAt(0);
        //    for (int i = 0; i < comp.GetLength(); i++)
        //    {
        //        Console.WriteLine(comp.GetAngajat(i));
        //    }
        //}

        static void scrie()
        {
            for (int i = 0; i < comp.GetLength(); i++)
            {
                w.WriteLine(comp.GetAngajat(i));
            }
        }

        static void Main(string[] args)
        {
            f = new StreamReader(@"../../angajati.txt");
            Citire();

            w = new StreamWriter("angdate.txt");
            SortByDate();
            scrie();
            w.Flush();

            w = new StreamWriter("angnume.txt");
           // SortByName();
            Companie<Angajat>.sort(comp.c);
            scrie();
            w.Flush();

            
            
        }
    }
}
