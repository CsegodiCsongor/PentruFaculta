using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetodeAvansateTeste
{
    public class Euler
    {
        int NumarPuncte;
        int[,] MatDeAdiacenta;
        List<int> path = new List<int>();

        public Euler(string FisierSursa)
        {
            TextReader FisierDate = new StreamReader(FisierSursa);
            NumarPuncte = int.Parse(FisierDate.ReadLine());
            MatDeAdiacenta = new int[NumarPuncte, NumarPuncte];
            string aux;
            while((aux=FisierDate.ReadLine())!=null)
            {
                MatDeAdiacenta[int.Parse(aux.Split(' ')[0]), int.Parse(aux.Split(' ')[1])] ++;
                MatDeAdiacenta[int.Parse(aux.Split(' ')[1]), int.Parse(aux.Split(' ')[0])] ++;
            }
            ViewMat();
            VerificaEulerian();
            //int start = GetStartPoint();
            //int[,] auxmat = new int[NumarPuncte, NumarPuncte];
            //for(int i=0;i<NumarPuncte;i++)
            //{
            //    for(int j=0;j<NumarPuncte; j++)
            //    {
            //        auxmat[i, j] = MatDeAdiacenta[i, j];
            //    }
            //}
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int q = 0; q < NumarPuncte; q++)
            //    {
            //        for (int j = 0; j < NumarPuncte; j++)
            //        {
            //            auxmat[q, j] = MatDeAdiacenta[q, j];
            //        }
            //    }
            //    GetPath(i, auxmat);
            //    ViewPath();
            //    path.Clear();
            //}
        }

        public int GetStartPoint()
        {
            for (int i = 0; i < NumarPuncte; i++)
            {
                int c = 0;
                for(int j=0;j<NumarPuncte; j++)
                {
                    if (MatDeAdiacenta[i, j] > 0) { c++; }
                }
                if (c % 2 == 1) { return i; }
            }
            return 0;
        }

        public void GetPath(int start,int [,] auxmat)
        {
            for(int i=0;i<NumarPuncte;i++)
            {
                if(auxmat[start,i]>0)
                {
                    auxmat[start, i] --;
                    auxmat[i, start] --;
                    GetPath(i, auxmat);
                }
            }
            path.Add(start);
        }

        public void VerificaEulerian()
        {
            int pare = 0, impare = 0;
            for(int i=0;i<NumarPuncte;i++)
            {
                int numarConexiuni = 0;
                for(int j=0;j<NumarPuncte;j++)
                {
                    if (MatDeAdiacenta[i, j] == 1) { numarConexiuni++; }
                }
                if(numarConexiuni!=0&&numarConexiuni%2==0){pare++;}
                if(numarConexiuni!=0&&numarConexiuni%2!=0){impare++;}
            }
            if(impare==0&&pare>0)
            {
                Console.WriteLine("Graful contine Ciclu");
            }
            else if(impare==2&&pare>0)
            {
                Console.WriteLine("Graful contine Lant");
            }
            else
            {
                Console.WriteLine("Graful nu contine nimic");
            }
        }

        public void ViewMat()
        {
            for(int i=0;i<NumarPuncte;i++)
            {
                for(int j=0;j<NumarPuncte;j++)
                {
                    Console.Write(MatDeAdiacenta[i, j]+" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ViewPath()
        {
            for(int i=0;i<path.Count;i++)
            {
                Console.Write(path[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
