using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRMI.Interfaces
{
    public interface ISudokuService
    {
        int[,] GenerarSudoku(int size);
        string MostrarTablero();
        bool InsertarNumero(int fila, int col, int valor);
        bool EstaCompleto();
    }
}
