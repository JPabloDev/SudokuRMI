using SudokuRMI.Interfaces;
using SudokuRMI.Models;
using System.Text;

namespace SudokuRMI.Services
{
    public class SudokuService : ISudokuService
    {
        private SudokuBoard _sudukoBoard;
        public int[,] GenerarSudoku(int size)
        {
            _sudukoBoard = new SudokuBoard(size);
            _sudukoBoard.GenerarTableroBase(size);
            return new int[size, size];
        }
      

        public string MostrarTablero()
        {
            if (_sudukoBoard == null) return "No hay Sudoku generado.\n";

            StringBuilder sb = new();
            for (int i = 0; i < _sudukoBoard.size; i++)
            {
                if (i % _sudukoBoard.subSize == 0)
                    sb.AppendLine(new string('-', _sudukoBoard.size * 2 + _sudukoBoard.subSize + 1));

                for (int j = 0; j < _sudukoBoard.size; j++)
                {
                    if (j % _sudukoBoard.subSize == 0) sb.Append("| ");
                    sb.Append(_sudukoBoard.board[i, j] == 0 ? ". " : _sudukoBoard.board[i, j] + " ");
                }
                sb.AppendLine("|");
            }
            sb.AppendLine(new string('-', _sudukoBoard.size * 2 + _sudukoBoard.subSize + 1));
            return sb.ToString();
        }

        public bool InsertarNumero(int fila, int col, int valor)
        {
            return _sudukoBoard.EsValido(fila, col, valor);
        }

        public bool EstaCompleto()
        {
            if (_sudukoBoard == null) return false;
            for (int i = 0; i < _sudukoBoard.size; i++)
                for (int j = 0; j < _sudukoBoard.size; j++)
                    if (_sudukoBoard.board[i, j] == 0)
                        return false;
            return true;
        }
    }
}
