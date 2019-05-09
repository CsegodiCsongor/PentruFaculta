using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseItForAll
{
    public class Sorts<T> where T:IComparable
    {
        public static void Exchange(ref T a, ref T b)
        {
            T aux = a;
            a = b;
            b = aux;
        }


        public static void BubbleSort(T[] vector)
        {
            bool ok = false;
            while(!ok)
            {
                ok = true;
                for (int i = 0; i < vector.Length - 1; i++)
                {
                    if (vector[i].CompareTo(vector[i+1])>0)
                    {
                        Exchange(ref vector[i], ref vector[i + 1]);
                        ok = false;
                    }
                }
            }
            //return vector;
        }


        public static T[] InsertionSort(T[] vector)
        {
            for(int i=1;i<vector.Length;i++)
            {
                int j = i;
                int k = i;
                while(j>0 && vector[j-1].CompareTo(vector[k])>=0)
                {
                    j--;
                    Exchange(ref vector[j], ref vector[k]);
                    k = j;

                }
            }
            return vector;
        }


        public static T[] SelectionSort(T[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                int min = i;
                for (int j = i+1; j < vector.Length; j++)
                {
                    if (vector[j].CompareTo(vector[min]) < 0)
                    {
                        min = j;
                    }
                }
                Exchange(ref vector[i], ref vector[min]);
            }
            return vector;
        }


        public static T[] MergeSort(T[] vector)
        {
            MergeSortHelper(vector, 0, vector.Length-1);
            return vector;
        }

        private static void MergeSortHelper(T[] vector,int left, int right)
        {
            if(right>left)
            {
                int mid = (right + left) / 2;
                MergeSortHelper(vector, left, mid);
                MergeSortHelper(vector, mid + 1, right);
                Merge(vector, left, mid, right);
            }
        }

        private static void Merge(T[] vector, int left, int mid, int right)
        {
            T[] v = new T[right - left + 1];
            int i = left;
            int j = mid + 1;
            int k = 0;
            while (i <= mid && j <= right)
            {
                if (vector[i].CompareTo(vector[j]) <= 0)
                {
                    v[k++] = vector[i++];
                }
                else
                {
                    v[k++] = vector[j++];
                }
            }
            while (i <= mid)
            {
                v[k++] = vector[i++];
            }
            while (j <= right)
            {
                v[k++] = vector[j++];
            }
            for (i = 0; i < k; i++)
            {
                vector[left + i] = v[i];
            }
        }


        public static T[] QuickSort(T[] vector)
        {
            QuickSortHelper(vector, 0, vector.Length - 1);
            return vector;
        }

        private static void QuickSortHelper(T[]vector,int left,int right)
        {
            if(left<right)
            {
                int pivot = QuickPart(vector, left, right);
                QuickSortHelper(vector, left, pivot - 1);
                QuickSortHelper(vector, pivot + 1, right);
            }
        }

        private static int QuickPart(T[] vector,int left,int right)
        {
            T pivot = vector[right];
            int i = left;
            for(int j=left; j<right;j++)
            {
                if(vector[j].CompareTo(pivot)<=0)
                {
                    Exchange(ref vector[i],ref vector[j]);
                    i++;
                }
            }
            Exchange(ref vector[i], ref vector[right]);
            return i;
        }
    }
}
