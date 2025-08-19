using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRMI.Models
{
    public class SudokuBoard
    {
        public int[,] Celdas { get; set; }

        public SudokuBoard()
        {
            Celdas = new int[9, 9];
        }

        public void GenerarTableroBase()
        {
            // Tablero base sencillo
            Celdas[0, 0] = 5;
            Celdas[1, 3] = 6;
            Celdas[4, 4] = 7;
            Celdas[7, 5] = 1;
        }
    }
}
