using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectArhitecturaCsegodiCsongor
{

    class Program
    {
        public static int bazaOrigin, bazaRezult, lungime = 0;
        public static string nrInString;
        public static int[] nrInInt = new int[100];
        public static int[] rezultInInt = new int[100];
        public static int pe10=0;
        public static int indexDePe10 = 0;

        public static void Citire()
        {
            Console.Write("Introduceti numarul care vreti sa fie transformat:");
            nrInString = Console.ReadLine();
            Console.Write("Intrudeci din ce baza este numarul[0,35]:");
            bazaOrigin = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introducei in ce baza vreti sa fie transormat numarul[0,35]:");
            bazaRezult = Convert.ToInt32(Console.ReadLine());
        }


        public static void Corectare()
        {
            for (int i = 0; i < nrInString.Length; i++)
            {
                if (Char.IsLetter(nrInString[i]))
                {
                    if (nrInString[i] == 'A' || nrInString[i] == 'a') nrInInt[i] = 10;
                    if (nrInString[i] == 'B' || nrInString[i] == 'b') nrInInt[i] = 11;
                    if (nrInString[i] == 'C' || nrInString[i] == 'c') nrInInt[i] = 12;
                    if (nrInString[i] == 'D' || nrInString[i] == 'd') nrInInt[i] = 13;
                    if (nrInString[i] == 'E' || nrInString[i] == 'e') nrInInt[i] = 14;
                    if (nrInString[i] == 'F' || nrInString[i] == 'f') nrInInt[i] = 15;
                    if (nrInString[i] == 'G' || nrInString[i] == 'g') nrInInt[i] = 16;
                    if (nrInString[i] == 'H' || nrInString[i] == 'h') nrInInt[i] = 17;
                    if (nrInString[i] == 'I' || nrInString[i] == 'i') nrInInt[i] = 18;
                    if (nrInString[i] == 'J' || nrInString[i] == 'j') nrInInt[i] = 19;
                    if (nrInString[i] == 'K' || nrInString[i] == 'k') nrInInt[i] = 20;
                    if (nrInString[i] == 'L' || nrInString[i] == 'l') nrInInt[i] = 21;
                    if (nrInString[i] == 'M' || nrInString[i] == 'm') nrInInt[i] = 22;
                    if (nrInString[i] == 'N' || nrInString[i] == 'n') nrInInt[i] = 23;
                    if (nrInString[i] == 'O' || nrInString[i] == 'o') nrInInt[i] = 24;
                    if (nrInString[i] == 'P' || nrInString[i] == 'p') nrInInt[i] = 25;
                    if (nrInString[i] == 'Q' || nrInString[i] == 'q') nrInInt[i] = 26;
                    if (nrInString[i] == 'R' || nrInString[i] == 'r') nrInInt[i] = 27;
                    if (nrInString[i] == 'S' || nrInString[i] == 's') nrInInt[i] = 28;
                    if (nrInString[i] == 'T' || nrInString[i] == 't') nrInInt[i] = 29;
                    if (nrInString[i] == 'U' || nrInString[i] == 'u') nrInInt[i] = 30;
                    if (nrInString[i] == 'V' || nrInString[i] == 'v') nrInInt[i] = 31;
                    if (nrInString[i] == 'W' || nrInString[i] == 'w') nrInInt[i] = 32;
                    if (nrInString[i] == 'X' || nrInString[i] == 'x') nrInInt[i] = 33;
                    if (nrInString[i] == 'Y' || nrInString[i] == 'y') nrInInt[i] = 34;
                    if (nrInString[i] == 'Z' || nrInString[i] == 'z') nrInInt[i] = 35;
                    lungime++;
                }
                else { nrInInt[i] = Convert.ToInt32(nrInString[i]) - 48; lungime++; }
            }
        }


        public static bool Verificare()
        {
            for (int i = 0; i < lungime; i++)
            {
                if (nrInInt[i] >= bazaOrigin || bazaRezult>36 || bazaOrigin>36)
                {
                    return false;

                }
            }
            return true;
        }


        public static void Convertare ()
        {
           if(bazaOrigin==bazaRezult)
            {
                Console.Write("Numarul transformat este:");
                for (int i=0;i<nrInString.Length;i++)
                {
                    Console.Write(nrInString[i]);
                }
            }
           if(bazaOrigin!=10&&bazaRezult==10)
            {
                dePeCevaPe10(bazaOrigin);
                Console.WriteLine("Nr-u transformat este:" + pe10);

            }
           if(bazaOrigin!=10&&bazaRezult!=10)
            {
                dePeCevaPe10(bazaOrigin);
                dePe10PeCeva(bazaRezult);
                Console.Write("Numarul transformat este:");
                Scriere(); 

            }
           if(bazaOrigin==10&&bazaRezult!=10)
            {
                Recreare();
                dePe10PeCeva(bazaRezult);
                Console.Write("Numarul transformat este:");
                Scriere(); 
            }
        }


        public static void dePe10PeCeva(int r)
        {
            while (pe10>0)
            {
                rezultInInt[indexDePe10] = pe10 % r;
                pe10 = pe10 / r;
                indexDePe10++;
            }

        }


        public static void dePeCevaPe10(int o)
        {
            int index = -1;
            int putere = 1;
            for(int i=lungime-1;i>=0;i--)
            {
                index++;
                for(int j=0;j<index;j++)
                {
                    putere = putere * o;
                }
                putere = putere * nrInInt[i];
                pe10 = pe10 + putere;
                putere = 1;
            }                        
        }


        public static void Recreare()
        {
            for(int i=0;i<lungime;i++)
            {
                pe10 = pe10 * 10 + nrInInt[i];
                
            }

        }


        public static void Scriere()
        {
            for(int i=indexDePe10-1;i>=0;i--)
            {
                if(rezultInInt[i]<=9)
                {
                    Console.Write(rezultInInt[i]);
                }
                if (rezultInInt[i] == 10) { Console.Write("A"); }
                if (rezultInInt[i] == 11) { Console.Write("B"); }
                if (rezultInInt[i] == 12) { Console.Write("C"); }
                if (rezultInInt[i] == 13) { Console.Write("D"); }
                if (rezultInInt[i] == 14) { Console.Write("E"); }
                if (rezultInInt[i] == 15) { Console.Write("F"); }
                if (rezultInInt[i] == 16) { Console.Write("G"); }
                if (rezultInInt[i] == 17) { Console.Write("H"); }
                if (rezultInInt[i] == 18) { Console.Write("I"); }
                if (rezultInInt[i] == 19) { Console.Write("J"); }
                if (rezultInInt[i] == 20) { Console.Write("K"); }
                if (rezultInInt[i] == 21) { Console.Write("L"); }
                if (rezultInInt[i] == 22) { Console.Write("M"); }
                if (rezultInInt[i] == 23) { Console.Write("N"); }
                if (rezultInInt[i] == 24) { Console.Write("O"); }
                if (rezultInInt[i] == 25) { Console.Write("P"); }
                if (rezultInInt[i] == 26) { Console.Write("Q"); }
                if (rezultInInt[i] == 27) { Console.Write("R"); }
                if (rezultInInt[i] == 28) { Console.Write("S"); }
                if (rezultInInt[i] == 29) { Console.Write("T"); }
                if (rezultInInt[i] == 30) { Console.Write("U"); }
                if (rezultInInt[i] == 31) { Console.Write("V"); }
                if (rezultInInt[i] == 32) { Console.Write("W"); }
                if (rezultInInt[i] == 33) { Console.Write("X"); }
                if (rezultInInt[i] == 34) { Console.Write("Y"); }
                if (rezultInInt[i] == 35) { Console.Write("Z"); }
            }
        }

        static void Main(string[] args)
        {
            Citire();
            Corectare();
            if (Verificare() == true)
            {
                 Convertare();
               
            }
            else { Console.WriteLine("Ai dat nr prea mare pt baza {0} Sau ai dat o baza pe care nu pot lucra",bazaOrigin); }
            Console.ReadKey();
        }
    }
}
