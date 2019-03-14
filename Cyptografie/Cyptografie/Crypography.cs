using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyptografie
{
    public abstract class Cryptography
    {
        public abstract string message
        {
            get;
            set;
        }

        public abstract void ReadFromFile(string FileName);
        public abstract void WriteToFile(string FileName);
        public abstract void ShowMessageOnScreen();
    }
}
