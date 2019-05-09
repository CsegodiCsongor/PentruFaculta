using System;
using System.Collections.Generic;
using System.IO;

namespace MetodeAvansateTeste
{
    public class Edge
    {
        public int start, final, valoare;
        public Edge(int start,int final,int valoare)
        {
            this.start = start;
            this.final = final;
            this.valoare = valoare;
        }
    }

   public class Kruskall
   {
        int NumarPuncte;
        int[,] MatDeAdiacenta;
        List<Edge> muchii = new List<Edge>(); 

        public Kruskall(string kruskmatsrc)
        {
            TextReader FisierDate = new StreamReader(kruskmatsrc);
            NumarPuncte = int.Parse(FisierDate.ReadLine());
            MatDeAdiacenta = new int[NumarPuncte, NumarPuncte];
            string buffer;
            while((buffer=FisierDate.ReadLine())!=null)
            {
                int s = int.Parse(buffer.Split(' ')[0])-1;
                int e = int.Parse(buffer.Split(' ')[1])-1;
                int v = int.Parse(buffer.Split(' ')[2]);
                MatDeAdiacenta[s, e] = 1;
                MatDeAdiacenta[e, s] = 1;
                muchii.Add(new Edge(s, e, v));
            }
            //Console.WriteLine(ViewEdges());
            //Console.WriteLine("\n");
            //Console.WriteLine(ViewMat());
            SortareMuchii();
            //Console.WriteLine(ViewEdges());
            //Console.WriteLine("\n");
            DetermArboreMinim();
        }

        public void DetermArboreMinim()
        {
            List<Edge> arbore = new List<Edge>();
            arbore.Add(muchii[0]);
            for(int i=1;i<muchii.Count;i++)
            {
                arbore.Add(muchii[i]);
                int [,] mataux = new int[NumarPuncte,NumarPuncte];
                //for (int j = 0; j < NumarPuncte; j++)
                //{
                //    for (int q = 0; q < NumarPuncte; q++)
                //    {
                //        mataux[j, q] = 0;
                //    }
                //}
                for (int j=0;j<arbore.Count;j++)
                {
                    mataux[arbore[j].final, arbore[j].start] = 1;
                    mataux[arbore[j].start, arbore[j].final] = 1;
                }
                if(ECiclu(mataux,muchii[i].start))
                {
                    arbore.RemoveAt(arbore.Count-1);
                }
            }
            for(int i=0; i<arbore.Count;i++)
            {
                Console.WriteLine((char)(arbore[i].start + 65) + " " + (char)(arbore[i].final + 65) + " " + arbore[i].valoare);
                //Console.WriteLine(arbore[i].start + " " + arbore[i].final + " " + arbore[i].valoare);
            }
        }

        public bool ECiclu(int[,] MatAux, int start)
        {
            List<int> aux = new List<int>();
            bool[] vizitat = new bool[NumarPuncte];
            aux.Add(start);
            //vizitat[start] = true;
            while (aux.Count != 0)
            {
                int curent = aux[0];
                vizitat[curent] = true;
                for (int i = 0; i < NumarPuncte; i++)
                {
                    if (!vizitat[i] && MatAux[curent, i] == 1)
                    {
                        if (aux.Contains(i))
                        {
                            return true;
                        }
                        else
                        {
                            aux.Add(i);
                        }
                    }
                }
                aux.RemoveAt(0);
            }
            return false;
        }

        public void SortareMuchii()
        {
            muchii.Sort(delegate (Edge a, Edge b)
            {
                return a.valoare.CompareTo(b.valoare);
            });
        }

        public string ViewMat()
        {
            string h = "";
            for(int i=0;i<NumarPuncte;i++)
            {
                for(int j=0;j<NumarPuncte;j++)
                {
                    h += MatDeAdiacenta[i, j].ToString()+" ";
                }
                h += "\n";
            }
            return h;
        }

        public string ViewEdges()
        {
            string h = "";
            for(int i=0;i<muchii.Count;i++)
            {
                h += muchii[i].start+" "+muchii[i].final+" "+muchii[i].valoare+"\n";
            }
            return h;
        }
   }
}
