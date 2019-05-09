using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetodeAvansateTeste
{
    public class Hamilton
    {
        public int [,] MatDeAdiacenta;
        public int NumarPuncte;

        public Hamilton(string FisierSursa)
        {
            TextReader FisierDate = new StreamReader(FisierSursa);
            NumarPuncte = int.Parse(FisierDate.ReadLine());
            MatDeAdiacenta = new int[NumarPuncte, NumarPuncte];
            string aux;
            while((aux=FisierDate.ReadLine())!=null)
            {
                MatDeAdiacenta[int.Parse(aux.Split(' ')[0]), int.Parse(aux.Split(' ')[1])] = 1;
                MatDeAdiacenta[int.Parse(aux.Split(' ')[1]), int.Parse(aux.Split(' ')[0])] = 1;
            }
            bool[] Vizitat = new bool[NumarPuncte];
            int nr = 0;
            List<int> Puncte = new List<int>();
            for (int i=0;i<NumarPuncte;i++)
            {
                nr = 1;
                Vizitat[i] = true;
                Puncte.Add(i);

                BackTrack(i, Vizitat,nr, Puncte,i);

                Vizitat[i] = false;
                Puncte.RemoveAt(Puncte.Count - 1);
            }
            
        }

        public void BackTrack(int punctCurent,bool[]vizitat,int numarIterare,List<int> Puncte,int punctInitial)
        {
            if (numarIterare == NumarPuncte)
            {
                Console.WriteLine("Drum hamiltonian gasit");
                for(int i=0;i<Puncte.Count;i++)
                {
                    Console.Write(Puncte[i]+" ");
                }
                if (MatDeAdiacenta[punctCurent, punctInitial] == 1)
                {
                    Console.Write("Este Ciclu!");
                }
                else
                {
                    Console.Write("Este Lant!");
                }
                Console.WriteLine();

            }
            else
            {
                for (int i = 0; i < NumarPuncte; i++)
                {
                    if (MatDeAdiacenta[punctCurent, i] == 1&&!vizitat[i])
                    {
                        vizitat[i] = true;
                        Puncte.Add(i);

                        BackTrack(i, vizitat,numarIterare+1,Puncte,punctInitial);

                        vizitat[i] = false;
                        Puncte.RemoveAt(Puncte.Count-1);
                    }
                }
            }
        }
    }
}
