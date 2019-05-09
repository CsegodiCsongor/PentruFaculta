using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsegodiCsongorPOPO
{
    public interface IGets
    {
        string GetFName();
        string GetLName();
        DateTime GetDate();
    }

    public class Angajat : IGets, IComparable<Angajat>
    {
        string nume;
        string prenume;
        DateTime d = new DateTime();
        public Angajat(string nume, string prenume, DateTime d)
        {
            this.nume = nume;
            this.prenume = prenume;
            this.d = d;
        }

        public override string ToString()
        {
            return nume + " " + prenume + " " + d.ToString("yyyy/MM/dd");
        }

        public string GetFName()
        {
            return prenume;
        }
        public string GetLName()
        {
            return nume;
        }
        public DateTime GetDate()
        {
            return d;
        }

        public int CompareTo(Angajat other)
        {
            return (GetLName()+GetFName()).CompareTo(other.GetLName()+other.GetFName());
        }

    }
}
