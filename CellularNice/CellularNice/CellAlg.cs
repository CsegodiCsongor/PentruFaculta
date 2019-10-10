using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularNice
{
    public class CellAlg
    {
        int[,] cel;

        public int[,] RunAlg(int[,] initCel)
        {
            int n = initCel.GetLength(0);
            cel = new int[n + 2, n + 2];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    cel[i + 1, j + 1] = initCel[i, j];
                }
            }

            int[,] auxCell = new int[n, n];
            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    auxCell[i - 1, j - 1] = cel[i - 1, j] + cel[i+1, j] + cel[i , j+1] + cel[i, j-1];
                }
            }

            return auxCell;
        }
    }
}
