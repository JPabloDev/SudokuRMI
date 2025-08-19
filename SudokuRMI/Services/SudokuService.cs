using SudokuRMI.Interfaces;
using SudokuRMI.Models;
using System.Text;

namespace SudokuRMI.Services
{
    public class SudokuService : ISudokuService
    {
        private SudokuBoard _tablero;

        public void GenerarSudoku()
        {
            _tablero = new SudokuBoard();
            _tablero.GenerarTableroBase();
        }

        public string MostrarTablero()
        {
            if (_tablero == null) return "No hay Sudoku generado.\n";

            StringBuilder sb = new();
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0) sb.AppendLine("+-------+-------+-------+");
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0) sb.Append("| ");
                    sb.Append(_tablero.Celdas[i, j] == 0 ? ". " : _tablero.Celdas[i, j] + " ");
                }
                sb.AppendLine("|");
            }
            sb.AppendLine("+-------+-------+-------+");
            return sb.ToString();
        }

        public bool InsertarNumero(int fila, int col, int valor)
        {
            if (_tablero == null) return false;
            if (_tablero.Celdas[fila, col] != 0) return false;

            // validar fila
            for (int i = 0; i < 9; i++)
                if (_tablero.Celdas[fila, i] == valor) return false;

            // validar col
            for (int i = 0; i < 9; i++)
                if (_tablero.Celdas[i, col] == valor) return false;

            // validar subcuadro
            int startRow = (fila / 3) * 3;
            int startCol = (col / 3) * 3;
            for (int i = startRow; i < startRow + 3; i++)
                for (int j = startCol; j < startCol + 3; j++)
                    if (_tablero.Celdas[i, j] == valor) return false;

            _tablero.Celdas[fila, col] = valor;
            return true;
        }

        public bool EstaCompleto()
        {
            if (_tablero == null) return false;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (_tablero.Celdas[i, j] == 0)
                        return false;
            return true;
        }
    }
}
