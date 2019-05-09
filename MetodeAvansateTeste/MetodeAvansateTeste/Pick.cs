using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetodeAvansateTeste
{
    public struct Point
    {
        public double x;
        public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Linie
    {
        public Point p1, p2;
    }

    public class Pick
    {
        public static bool PeLinie(Linie segment, Point punct)
        {        //check whether p is on the line or not
            if (punct.x <= Math.Max(segment.p1.x, segment.p2.x) && punct.x >= Math.Min(segment.p1.x, segment.p2.x) &&
               (punct.y <= Math.Max(segment.p1.y, segment.p2.y) && punct.y >= Math.Min(segment.p1.y, segment.p2.y)))
                return true;

            return false;
        }

        public static int Directie(Point a, Point b, Point c)
        {
            int val = (int)(b.y - a.y) * (int)(c.x - b.x) - (int)(b.x - a.x) * (int)(c.y - b.y);
            if (val == 0)
                return 0;           //colinear
            else if (val < 0)
                return 2;          //anti-clockwise direction
            return 1;          //clockwise direction
        }

        public static bool Intersectie(Linie linia1, Linie linia2)
        {
            //four direction for two lines and points of other line
            int dir1 = Directie(linia1.p1, linia1.p2, linia2.p1);
            int dir2 = Directie(linia1.p1, linia1.p2, linia2.p2);
            int dir3 = Directie(linia2.p1, linia2.p2, linia1.p1);
            int dir4 = Directie(linia2.p1, linia2.p2, linia1.p2);

            if (dir1 != dir2 && dir3 != dir4)
                return true;           //they are intersecting
            if (dir1 == 0 && PeLinie(linia1, linia2.p1))        //when p2 of line2 are on the line1
                return true;
            if (dir2 == 0 && PeLinie(linia1, linia2.p2))         //when p1 of line2 are on the line1
                return true;
            if (dir3 == 0 && PeLinie(linia2, linia1.p1))       //when p2 of line1 are on the line2
                return true;
            if (dir4 == 0 && PeLinie(linia2, linia1.p2)) //when p1 of line1 are on the line2
                return true;
            return false;
        }

        public static bool InPolygon(Point[] polygon, int numarLaturi, Point punct)
        {
            //if (n < 3)
            // return false;                  //when polygon has less than 3 edge, it is not polygon
            Linie exline;   //create a point at infinity, y is same as point p
            exline.p1 = punct;
            exline.p2.x = int.MaxValue;
            exline.p2.y = punct.y;
            int count = 0;
            int i = 0;
            do
            {
                Linie side; /*= { poly[i], poly[(i + 1) % n] }*/     //forming a line from two consecutive points of poly
                side.p1 = polygon[i];
                side.p2 = polygon[(i + 1) % numarLaturi];
                if (Intersectie(side, exline))
                {          //if side is intersects exline
                    if (Directie(side.p1, punct, side.p2) == 0)
                        return PeLinie(side, punct);
                    count++;
                }
                i = (i + 1) % numarLaturi;
            } while (i != 0);
            return count % 2 == 1;             //when count is odd
        }

        public bool PeLinie(Point punctLinie1, Point punctLinie2, Point punctLinie3)
        {
            double a = punctLinie2.y - punctLinie1.y;
            double b = punctLinie1.x - punctLinie2.x;
            double c = a * (punctLinie1.x) + b * (punctLinie1.y);

            return a * punctLinie3.x + b * punctLinie3.y == c
                &&punctLinie3.x<=Math.Max(punctLinie1.x,punctLinie2.x)&&punctLinie3.x>=Math.Min(punctLinie1.x,punctLinie2.x)
                && punctLinie3.y <= Math.Max(punctLinie1.y, punctLinie2.y) && punctLinie3.y >= Math.Min(punctLinie1.y, punctLinie2.y);
        }

        public int[,] mat;
        public Point[] Puncte;
        public int NumarPuncte=5;
        public int DimMat;
        int PuncteInterior=0;
        int PuncteLatura;
        float Arie;
        public Pick(string FisierSursa)
        {
            string aux;
            TextReader FisierDate = new StreamReader(FisierSursa);
            aux = FisierDate.ReadLine();
            DimMat = int.Parse(aux.Split(' ')[0]);
            NumarPuncte = int.Parse(aux.Split(' ')[1]);
            mat = new int[DimMat, DimMat];
            Puncte = new Point[NumarPuncte];
            PuncteLatura = NumarPuncte;
            for(int i=0;i<NumarPuncte;i++)
            {
                aux = FisierDate.ReadLine();
                Puncte[i] = new Point(int.Parse(aux.Split(' ')[1]), int.Parse(aux.Split(' ')[0]));
                mat[(int)Puncte[i].x, (int)Puncte[i].y]=1;
                
            }
            for(int i=0;i<DimMat;i++)
            {
                for(int j=0;j<DimMat;j++)
                {
                    if (mat[j, i] != 1)
                    {
                        for (int x = 0; x < NumarPuncte-1; x++)
                        {
                            if(PeLinie(Puncte[x],Puncte[x+1],new Point(j,i)))
                            {
                                mat[j, i] = 3;
                                PuncteLatura++;
                            }
                        }
                        if (PeLinie(Puncte[0], Puncte[NumarPuncte-1], new Point(j, i)))
                        {
                            mat[j, i] = 3;
                            PuncteLatura++;
                        }
                        if (InPolygon(Puncte, NumarPuncte, new Point(j, i)))
                        {
                            PuncteInterior++;
                            mat[j, i] = 2;
                        }
                    }
                }
            }
            PuncteInterior += NumarPuncte-PuncteLatura;
            Console.WriteLine(PuncteLatura + " " + PuncteInterior);
            Arie = PuncteLatura / 2 + PuncteInterior - 1;
            Console.WriteLine("Aria este: " + Arie);
            ViewMat();

        }

        public void ViewMat()
        {
            for(int i=0;i<DimMat;i++)
            {
                for(int j=0;j<DimMat;j++)
                {
                    Console.Write(mat[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
