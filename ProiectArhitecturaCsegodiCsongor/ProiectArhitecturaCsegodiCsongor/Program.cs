using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectArhitecturaCsegodiCsongor
{

    class Program
    {
        public static string nrCitit;

        public static int lungimeZecim = 0;
        public static string nrInZecim;
        public static int[] nrInZecimInt = new int[100];
        public static double[] rezultatInZecim = new double[100];
        public static double peZecim = 0;
        public static int indexPeZecim = 0;

        public static bool punct = false;
        public static int bazaOrigin, bazaRezult;

        public static int lungime = 0;
        public static string nrInString;
        public static int[] nrInInt = new int[100];
        public static int[] rezultInInt = new int[100];
        public static int pe10=0;
        public static int indexDePe10 = 0;

        public static void Citire()
        {
            Console.Write("Introduceti numarul care vreti sa fie transformat:");
            nrCitit = Console.ReadLine();
            Console.Write("Intrudeci din ce baza este numarul[0,36]:");
            bazaOrigin = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introducei in ce baza vreti sa fie transormat numarul[0,36]:");
            bazaRezult = Convert.ToInt32(Console.ReadLine());
        }

        public static void despartire ()
        {
            int i = 0;
            for(int k=0;k<nrCitit.Length;k++)
            {
                if (nrCitit[k] == '.') { punct = true; }
            }
            if (punct == true)
            {
                while (nrCitit[i] != '.')
                {
                    if (i == nrCitit.Length - 1)
                    {
                        break;
                    }
                    i++;
                }
                nrInString = nrCitit.Remove(i);
                nrInZecim = nrCitit.Remove(0, i + 1);
            }
            else { nrInString = nrCitit; }
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
            if (punct == true) {
                
                for (int i = 0; i < nrInZecim.Length; i++)
                {
                    if (Char.IsLetter(nrInZecim[i]))
                    {
                        if (nrInZecim[i] == 'A' || nrInZecim[i] == 'a') nrInZecimInt[i] = 10;
                        if (nrInZecim[i] == 'B' || nrInZecim[i] == 'b') nrInZecimInt[i] = 11;
                        if (nrInZecim[i] == 'C' || nrInZecim[i] == 'c') nrInZecimInt[i] = 12;
                        if (nrInZecim[i] == 'D' || nrInZecim[i] == 'd') nrInZecimInt[i] = 13;
                        if (nrInZecim[i] == 'E' || nrInZecim[i] == 'e') nrInZecimInt[i] = 14;
                        if (nrInZecim[i] == 'F' || nrInZecim[i] == 'f') nrInZecimInt[i] = 15;
                        if (nrInZecim[i] == 'G' || nrInZecim[i] == 'g') nrInZecimInt[i] = 16;
                        if (nrInZecim[i] == 'H' || nrInZecim[i] == 'h') nrInZecimInt[i] = 17;
                        if (nrInZecim[i] == 'I' || nrInZecim[i] == 'i') nrInZecimInt[i] = 18;
                        if (nrInZecim[i] == 'J' || nrInZecim[i] == 'j') nrInZecimInt[i] = 19;
                        if (nrInZecim[i] == 'K' || nrInZecim[i] == 'k') nrInZecimInt[i] = 20;
                        if (nrInZecim[i] == 'L' || nrInZecim[i] == 'l') nrInZecimInt[i] = 21;
                        if (nrInZecim[i] == 'M' || nrInZecim[i] == 'm') nrInZecimInt[i] = 22;
                        if (nrInZecim[i] == 'N' || nrInZecim[i] == 'n') nrInZecimInt[i] = 23;
                        if (nrInZecim[i] == 'O' || nrInZecim[i] == 'o') nrInZecimInt[i] = 24;
                        if (nrInZecim[i] == 'P' || nrInZecim[i] == 'p') nrInZecimInt[i] = 25;
                        if (nrInZecim[i] == 'Q' || nrInZecim[i] == 'q') nrInZecimInt[i] = 26;
                        if (nrInZecim[i] == 'R' || nrInZecim[i] == 'r') nrInZecimInt[i] = 27;
                        if (nrInZecim[i] == 'S' || nrInZecim[i] == 's') nrInZecimInt[i] = 28;
                        if (nrInZecim[i] == 'T' || nrInZecim[i] == 't') nrInZecimInt[i] = 29;
                        if (nrInZecim[i] == 'U' || nrInZecim[i] == 'u') nrInZecimInt[i] = 30;
                        if (nrInZecim[i] == 'V' || nrInZecim[i] == 'v') nrInZecimInt[i] = 31;
                        if (nrInZecim[i] == 'W' || nrInZecim[i] == 'w') nrInZecimInt[i] = 32;
                        if (nrInZecim[i] == 'X' || nrInZecim[i] == 'x') nrInZecimInt[i] = 33;
                        if (nrInZecim[i] == 'Y' || nrInZecim[i] == 'y') nrInZecimInt[i] = 34;
                        if (nrInZecim[i] == 'Z' || nrInZecim[i] == 'z') nrInZecimInt[i] = 35;
                        lungimeZecim++;
                    }
                    else { nrInZecimInt[i] = Convert.ToInt32(nrInZecim[i]) - 48; lungimeZecim++; }

                }
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

            for (int i = 0; i < lungimeZecim; i++)
            {
                if (nrInZecimInt[i] >= bazaOrigin)
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
                for (int i = 0; i < nrInString.Length; i++)
                {
                    Console.Write(nrInString[i]);
                }
                if (punct == true)
                {
                    Console.Write(".");
                    for (int i = 0; i < nrInZecim.Length; i++)
                    {
                        Console.Write(nrInZecim[i]);
                    }
                }
                
            }
            else if(bazaOrigin!=10&&bazaRezult==10)
            {
                dePeCevaPe10(bazaOrigin);
                Console.WriteLine("Nr-u transformat este: {0}",pe10+peZecim);
             

            }
            else if(bazaOrigin!=10&&bazaRezult!=10)
            {
                dePeCevaPe10(bazaOrigin);
                dePe10PeCeva(bazaRezult);
                Console.Write("Numarul transformat este:");
                Scriere(); 

            }
            else if(bazaOrigin==10&&bazaRezult!=10)
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
            if(punct==true)
            { 
                while(peZecim!=1)
                {
                    if (peZecim*r > 1)
                    {
                        
                        rezultatInZecim[indexPeZecim] =  Math.Floor(peZecim*r);
                        peZecim = peZecim*r -Math.Floor(peZecim*r);
                        indexPeZecim++;
                        if(indexPeZecim==99)
                        {
                            break;
                        }
                    }
                    else
                    {
                        
                        rezultatInZecim[indexPeZecim] = Math.Floor(peZecim * r);
                        peZecim = peZecim * r;
                        indexPeZecim++;
                        if (indexPeZecim == 99)
                        {
                            break;
                        }
                    }
                }
                
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
            
            if(punct==true)
            {
                int indexZecim = 0;
                for(int i=0;i<lungimeZecim;i++)
                {
                    indexZecim--;
                    peZecim =peZecim +nrInZecimInt[i]* Math.Pow(o, indexZecim);
                }
            }

        }


        public static void Recreare()
        {
            for(int i=0;i<lungime;i++)
            {
                pe10 = pe10 * 10 + nrInInt[i];
                
            }
            if(punct==true)
            {
                for(int i=lungimeZecim-1;i>=0;i--)
                {
                    peZecim = peZecim / 10 + nrInZecimInt[i];
                }
                peZecim = peZecim / 10;
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

            if (punct == true)
            {
                Console.Write(".");
                for (int i = 0; i < indexPeZecim; i++)
                {
                    if (rezultatInZecim[i] <= 9)
                    {
                        Console.Write(rezultatInZecim[i]);
                    }
                    if (rezultatInZecim[i] == 10) { Console.Write("A"); }
                    if (rezultatInZecim[i] == 11) { Console.Write("B"); }
                    if (rezultatInZecim[i] == 12) { Console.Write("C"); }
                    if (rezultatInZecim[i] == 13) { Console.Write("D"); }
                    if (rezultatInZecim[i] == 14) { Console.Write("E"); }
                    if (rezultatInZecim[i] == 15) { Console.Write("F"); }
                    if (rezultatInZecim[i] == 16) { Console.Write("G"); }
                    if (rezultatInZecim[i] == 17) { Console.Write("H"); }
                    if (rezultatInZecim[i] == 18) { Console.Write("I"); }
                    if (rezultatInZecim[i] == 19) { Console.Write("J"); }
                    if (rezultatInZecim[i] == 20) { Console.Write("K"); }
                    if (rezultatInZecim[i] == 21) { Console.Write("L"); }
                    if (rezultatInZecim[i] == 22) { Console.Write("M"); }
                    if (rezultatInZecim[i] == 23) { Console.Write("N"); }
                    if (rezultatInZecim[i] == 24) { Console.Write("O"); }
                    if (rezultatInZecim[i] == 25) { Console.Write("P"); }
                    if (rezultatInZecim[i] == 26) { Console.Write("Q"); }
                    if (rezultatInZecim[i] == 27) { Console.Write("R"); }
                    if (rezultatInZecim[i] == 28) { Console.Write("S"); }
                    if (rezultatInZecim[i] == 29) { Console.Write("T"); }
                    if (rezultatInZecim[i] == 30) { Console.Write("U"); }
                    if (rezultatInZecim[i] == 31) { Console.Write("V"); }
                    if (rezultatInZecim[i] == 32) { Console.Write("W"); }
                    if (rezultatInZecim[i] == 33) { Console.Write("X"); }
                    if (rezultatInZecim[i] == 34) { Console.Write("Y"); }
                    if (rezultatInZecim[i] == 35) { Console.Write("Z"); }
                }
            }
        }

        static void Main(string[] args)
        {
             Citire();
             despartire();
             Corectare();
             if (Verificare() == true)
             {
                 Convertare();

             }
             else { Console.WriteLine("ai dat nr prea mare pt baza {0} sau ai dat o baza pe care nu pot lucra", bazaOrigin); }
            Console.Read();
            
           
        }
    }
}
