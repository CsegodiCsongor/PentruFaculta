using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetodeAvansateTeste
{
    public class Dijkstra
    {
        int NumarPuncte;
        int[,] MatDeAdiacenta;
        int[] DistanteMinime;
        //public int minCost = int.MaxValue;
        //List<int> sp = new List<int>();
        public Dijkstra(string FisierSursa, int punctPornire)
        {
            TextReader FisierDate = new StreamReader(FisierSursa);
            NumarPuncte=int.Parse(FisierDate.ReadLine());
            MatDeAdiacenta = new int[NumarPuncte, NumarPuncte];
            DistanteMinime = new int[NumarPuncte];
            for(int i=0;i<NumarPuncte;i++)
            {
                DistanteMinime[i] = int.MaxValue;
            }
            DistanteMinime[punctPornire] = 0;
            string aux;
            while((aux=FisierDate.ReadLine())!=null)
            {
                MatDeAdiacenta[int.Parse(aux.Split(' ')[0]), int.Parse(aux.Split(' ')[1])] = int.Parse(aux.Split(' ')[2]);
                MatDeAdiacenta[int.Parse(aux.Split(' ')[1]), int.Parse(aux.Split(' ')[0])] = int.Parse(aux.Split(' ')[2]);
            }
            ViewMat();
            CalculDrumMinim(punctPornire);
            ViewDist();

            //bool[] visited=new bool[NumarPuncte];
            //visited[0] = true;
            //List<int> auxL = new List<int>();
            //auxL.Add(0);
            //ShortestPathFromAtoB(0, 6, 0, visited, auxL);
            //for(int i=0;i<sp.Count;i++)
            //{
            //    Console.Write(sp[i] + " ");
            //}
        }

        //public void ShortestPathFromAtoB(int startVertex,int destVertex,int cost,bool[]visited,List<int> vert)
        //{
        //    if(startVertex==destVertex)
        //    {
        //        if(cost<minCost)
        //        {
        //            Console.WriteLine(minCost + " > " + cost);
        //            minCost = cost;
        //            sp.Clear();
        //            for(int i=0;i<vert.Count;i++)
        //            {
        //                sp.Add(vert[i]);
        //            }
        //        }
        //    }
            //for(int i=0;i<NumarPuncte;i++)
            //{
            //    if(MatDeAdiacenta[startVertex,i]!=0&&!visited[i])
            //    {
            //        visited[i] = true;
            //        cost += MatDeAdiacenta[startVertex, i];
            //        vert.Add(i);

            //        ShortestPathFromAtoB(i, destVertex, cost, visited, vert);

            //        visited[i] = false;
            //        cost -= MatDeAdiacenta[startVertex, i];
            //        vert.RemoveAt(vert.Count - 1);
            //    }
            //}
        //}

        public void CalculDrumMinim(int punctInitial)
        {
            List<int> aux = new List<int>();
            aux.Add(punctInitial);
            bool[] vizitat = new bool[NumarPuncte];
            vizitat[punctInitial] = true;
            while(aux.Count!=0)
            {
                int punctCurent = aux[0];
                aux.RemoveAt(0);
                for(int i=0;i<NumarPuncte;i++)
                {
                    if(MatDeAdiacenta[punctCurent,i]!=0)
                    {
                        if(!vizitat[i])
                        {
                            aux.Add(i);
                            vizitat[i] = true;
                        }

                        if(DistanteMinime[punctCurent]+MatDeAdiacenta[punctCurent,i]
                            <DistanteMinime[i])
                        {
                            DistanteMinime[i] = 
                                DistanteMinime[punctCurent] + MatDeAdiacenta[punctCurent, i];
                        }
                        if(DistanteMinime[i]+MatDeAdiacenta[punctCurent,i]
                            <DistanteMinime[punctCurent])
                        {
                            DistanteMinime[punctCurent] = 
                                DistanteMinime[i] + MatDeAdiacenta[punctCurent, i];
                        }
                    }
                }
            }
        }

        public void ViewDist()
        {
            for (int i = 0; i < NumarPuncte; i++)
            {
                Console.Write(DistanteMinime[i] + " ");
            }
            Console.WriteLine();
        }

        public void ViewMat()
        {
            for(int i=0;i<NumarPuncte;i++)
            {
                for(int j=0;j<NumarPuncte; j++)
                {
                    Console.Write(MatDeAdiacenta[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
