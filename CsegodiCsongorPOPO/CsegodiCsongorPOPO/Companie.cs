using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsegodiCsongorPOPO
{
    public class Companie<T>
    {
        public T [] c;
        public Companie()
        {
            c = new T[0];
        }

        public void Add(T x)
        {
            T[] aux = new T[c.Length + 1];
            for(int i=0;i<c.Length;i++)
            {
                aux[i] = c[i];
            }
            aux[c.Length] = x;
            c = new T[aux.Length];
            for(int i=0;i<c.Length;i++)
            {
                c[i] = aux[i];
            }
        }
        public void RemoveAt(int i)
        {
            if (i < c.Length && i >= 0)
            {
                T[] aux = new T[c.Length - 1];
                int k = 0;
                for (int j = 0; j < c.Length; j++)
                {
                    if (j != i)
                    {
                        aux[k] = c[j];
                        k++;
                    }
                }
                c = new T[aux.Length];
                for (int j = 0; j < c.Length; j++)
                {
                    c[j] = aux[j];
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public int GetLength()
        {
            return c.Length;
        }

        public void ChangeAngajat(int i,int j)
        {
            if (i >= 0 && i < c.Length && j >= 0 && j < c.Length)
            {
                T d = c[i];
                c[i] = c[j];
                c[j] = d;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public T GetAngajat(int i)
        {
            if (i < c.Length && i >= 0)
            {
                return c[i];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void sort <T>(T[]a)where T:IComparable<T>
        {

            for(int i=0;i<a.Length;i++)
            {
                for(int j=i;j<a.Length;j++)
                {
                    if(a[i].CompareTo(a[j])>0)
                    {
                        T aux = a[i];
                        a[i] = a[j];
                        a[j] = aux;
                    }
                }
            }
        }


    }
}
