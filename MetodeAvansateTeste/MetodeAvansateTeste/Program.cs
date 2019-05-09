using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetodeAvansateTeste
{
    class Program
    {
        static Kruskall k;
        static Dijkstra d;
        static ColorMap cm;
        static Hamilton hm;
        static Euler eu;
        static Prim pr;
        static Pick pi;
        public static void LoadKruskall()
        {
            string b = "../../Krusk.txt";
            k = new Kruskall(b);
        }

        public static void LoadDijkstra()
        {
            string b = "../../Dijk.txt";
            d = new Dijkstra(b, 0);
        }

        public static void LoadMapColorin()
        {
            //string b = "../../MapMat.txt";
           // cm = new ColorMap(b);
        }

        public static void LoadIsHamil()
        {
            string b = "../../HamilMat.txt";
            hm = new Hamilton(b);
        }

        public static void LoadIsEuler()
        {
            string b = "../../EulerMat.txt";
            eu = new Euler(b);
        }

        public static void LoadPrim()
        {
            string b = "../../Prim.txt";
            pr = new Prim(b, 0);
        }

        public static void LoadPick()
        {
            string v = "../../PickT.txt";
            pi = new Pick(v);
        }

        static void Main(string[] args)
        {
            LoadPick();
        }
    }
}
