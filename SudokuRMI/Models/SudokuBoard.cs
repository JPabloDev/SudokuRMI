using System.Drawing;

namespace SudokuRMI.Models
{
    public class SudokuBoard
    {
        private readonly Random _random = new Random();
        public int size { get; set; }
        public int subSize { get; set; }
        public int[,] board { get; set; }

        public SudokuBoard(int tamaño)
        {
            size = tamaño;
            subSize = (int)Math.Sqrt(tamaño);
        }

        public int[,] GenerarTableroBase(int size)
        {
            board = new int[size, size];
            RellenarTablero(size);

            // Vaciar algunas celdas para que sea jugable
            int vaciar;
            if (size == 4) vaciar = 4;       // dificultad baja
            else if (size == 9) vaciar = 30; // dificultad media
            else vaciar = 30;                // dificultad más alta en 16x16

            EliminarCeldas(size, vaciar);

            return board;
        }

        // Genera un tablero válido completo usando backtracking
        private bool RellenarTablero(int size)
        {
            for (int fila = 0; fila < size; fila++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (board[fila, col] == 0)
                    {
                        List<int> numeros = Enumerable.Range(1, size).OrderBy(x => _random.Next()).ToList();

                        foreach (var num in numeros)
                        {
                            if (EsValido(fila, col, num))
                            {
                                board[fila, col] = num;

                                if (RellenarTablero(size))
                                    return true;

                                board[fila, col] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        // Elimina casillas al azar para dejar la matriz con los espacios a rellenar
        private void EliminarCeldas(int size, int cantidad)
        {
            int eliminados = 0;
            while (eliminados < cantidad)
            {
                int fila = _random.Next(size);
                int col = _random.Next(size);

                if (board[fila, col] != 0)
                {
                    board[fila, col] = 0;
                    eliminados++;
                }
            }
        }

        // Verifica si un número puede ir en una celda
        public bool EsValido(int fila, int col, int valor)
        {
            // Verificar fila y columna
            for (int i = 0; i < size; i++)
            {
                if (board[fila, i] == valor || board[i, col] == valor)
                    return false;
            }

            // Verificar subcuadro
            int inicioFila = (fila / subSize) * subSize;
            int inicioCol = (col / subSize) * subSize;

            for (int i = 0; i < subSize; i++)
            {
                for (int j = 0; j < subSize; j++)
                {
                    if (board[inicioFila + i, inicioCol + j] == valor)
                        return false;
                }
            }

            board[fila, col] = valor;

            return true;
        }
    }
}
