using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseItForAll
{
    public class Stack<T> where T : IComparable
    {

        public delegate int Icomparer(T a, T b);
        T[] elements;
        int lenght;

        public int Count
        {
            get
            {
                return lenght;
            }
        }

        public Stack()
        {
            elements = new T[0];
            lenght = 0;
        }

        public void AddBegining(T newElement)
        {
            AddAtIndex(newElement, 0);
        }

        public void Add(T newElement)
        {
            AddEnd(newElement);
        }

        public void Remove()
        {
            RemoveEnd();
        }

        public void AddEnd(T newElement)
        {
            AddAtIndex(newElement, lenght);
        }

        public void AddAtIndex(T newElement, int index)
        {
            T[] aux = new T[lenght + 1];
            for (int i = 0; i < index; i++)
            {
                aux[i] = elements[i];
            }
            aux[index] = newElement;
            for (int i = index; i < lenght; i++)
            {
                aux[i + 1] = elements[i];
            }
            lenght++;
            elements = new T[lenght];
            elements = aux;
        }

        public void RemvoveBegining()
        {
            RemoveAtIndex(0);
        }

        public void RemoveEnd()
        {
            RemoveAtIndex(lenght - 1);
        }

        public void RemoveAtIndex(int index)
        {
            T[] aux = new T[lenght - 1];
            for (int i = 0; i < index; i++)
            {
                aux[i] = elements[i];
            }
            for (int i = index + 1; i < lenght; i++)
            {
                aux[i - 1] = elements[i];
            }
            lenght--;
            elements = new T[lenght];
            elements = aux;
        }

        public override string ToString()
        {
            string aux = "";
            for (int i = 0; i < lenght; i++)
            {
                aux = aux + elements[i].ToString() + " ";
            }
            return aux;
        }

        public void Sort(Icomparer comp)
        {
            for (int i = 0; i < lenght; i++)
            {
                for (int j = i + 1; j < lenght; j++)
                {
                    if (comp(elements[i], elements[j]) > 0)
                    {
                        T aux = elements[i];
                        elements[i] = elements[j];
                        elements[j] = aux;
                    }
                }
            }
        }

        public void Sort()
        {
            for (int i = 0; i < lenght; i++)
            {
                for (int j = i + 1; j < lenght; j++)
                {
                    if (elements[i].CompareTo(elements[j]) > 0)
                    {
                        T aux = elements[i];
                        elements[i] = elements[j];
                        elements[j] = aux;
                    }
                }
            }
        }
    }
}
