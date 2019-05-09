using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetodeAvansateTeste
{
    public class ColorMap
    {
        //Punctele vor fii numeroate de la 0
        //Fisierul sursa contine doar numarul de puncte si relatia intre puncte fiecre pe randuri separate
        //De Exemplu un fisier ar putea arata astfel: 5
        //                                            0 1
        //                                            0 2
        //                                            1 2
        //                                            1 4
        //                                            2 4
        //                                            4 3

        int[] CuloriPuncte; //Fiecare punct va avea o culoare
        int NumarPuncte;
        int[,] MatDeAdiacenta;

        public ColorMap(string FisierSursa,int PunctInitial)  //Constructorul cere un string cu destinatia fisierului sursa si un numar care ne spune de la care punct sa inceapa colorarea
        {
            TextReader FisierDate = new StreamReader(FisierSursa);
            NumarPuncte = int.Parse(FisierDate.ReadLine());     //Citim numarul de puncte din fisier
            MatDeAdiacenta = new int[NumarPuncte, NumarPuncte];
            CuloriPuncte = new int[NumarPuncte];
            for(int i=0;i<NumarPuncte;i++)
            {
                CuloriPuncte[i] = -1; //La inceput nici unul dintre puncte nu va avea o culoare si culorile fiind numerotate de la 0 fiecare punct va avea valoarea culorii egala cu 0
            }
            string aux;
            while((aux=FisierDate.ReadLine())!=null) //Citeste continutul fisierului sursa in aux cat timp continutul acestuia nu este egal cu null.
            {
                MatDeAdiacenta[int.Parse(aux.Split(' ')[0]),int.Parse(aux.Split(' ')[1])] = 1; //Citim punctele din fisier
                MatDeAdiacenta[int.Parse(aux.Split(' ')[1]),int.Parse(aux.Split(' ')[0])] = 1;
            }
            Vizualizare();
            Colorare(PunctInitial);
            VizualizareCulori();
        }

        public void Colorare(int punct) //Cere doar punctul de la care sa incepa colorarea.
        {
            List<int> aux = new List<int>();
            bool[] visitat = new bool[NumarPuncte]; //Cu acest vector vom verifica care dintre puncte au fost deja folosite. Fiecare element al vectorului este initial false.
            visitat[punct] = true; //Punctul de pornire il punem ca vizitat.
            aux.Add(punct);        //Punem punctul in lista pentru a putea incepe parcurgerea in latime(BFS).
            while(aux.Count!=0)
            {
                int punctCurent = aux[0]; //Folosim lista ca o coada
                CuloriPuncte[punctCurent] = MinColor(punctCurent); 
                aux.RemoveAt(0);
                for(int i=0;i<NumarPuncte;i++)
                {
                    if(MatDeAdiacenta[punctCurent,i]==1&&!visitat[i]) //Adaugam in lista fiecare vecin nevizitat
                    {
                        aux.Add(i);
                        visitat[i] = true;
                    }
                }
            }
        }

        public int MinColor(int PunctCurent)
        {
            bool[] culoriGasite = new bool[NumarPuncte];
            for(int i=0;i<NumarPuncte;i++)
            {
                if(MatDeAdiacenta[PunctCurent,i]==1&&CuloriPuncte[i]!=-1) //Ne uitam la fiecare vecin al Punctului Curent si daca acesta
                {                                                         //arem  deja o culoare atunci punem culoarea true fiinde este deja in folosinta  
                    culoriGasite[CuloriPuncte[i]] = true;                 
                }
            }
            for(int i=0;i<NumarPuncte;i++)
            {
                if(culoriGasite[i]==false)              //Dupa ce marcam toate culorile vecinilor care sunt deja in folosinta
                {                                       //Returnam culoarea cu numarul cel mai mic care nu este in folosinta
                    return i;
                }
            }
            return 1;
        }

        public void Vizualizare()  //Vizualizam punctele
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

        public void VizualizareCulori()  //Vizualizam fiecare ce culoare are
        {
            for(int i=0;i<NumarPuncte;i++)
            {
                Console.WriteLine("Culoarea punctului "+i+" este " + CuloriPuncte[i]);
            }
        }
    }
}
