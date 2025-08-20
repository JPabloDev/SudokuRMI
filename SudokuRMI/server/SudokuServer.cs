using SudokuRMI.Services;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SudokuRMI.server
{
    public class SudokuServer
    {
        private readonly SudokuService sudoku = new SudokuService();

        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Servidor Sudoku escuchando en puerto 5000...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string response = ProcesarRequest(request);

                byte[] data = Encoding.UTF8.GetBytes(response);
                stream.Write(data, 0, data.Length);

                client.Close();
            }
        }

        private string ProcesarRequest(string request)
        {
            string[] parts = request.Split(' ');
            switch (parts[0])
            {
                case "GENERAR":
                    int size = parts.Length > 1 ? int.Parse(parts[1]) : 9;
                    sudoku.GenerarSudoku(size);
                    return $"Sudoku {size}x{size} generado!\n";
                case "MOSTRAR":
                    return sudoku.MostrarTablero();
                case "INSERTAR":
                    int fila = int.Parse(parts[1]);
                    int col = int.Parse(parts[2]);
                    int val = int.Parse(parts[3]);
                    return sudoku.InsertarNumero(fila, col, val) ? "OK\n" : "ERROR\n";
                case "COMPLETO":
                    return sudoku.EstaCompleto() ? "SI\n" : "NO\n";
                default:
                    return "Comando inválido\n";
            }
        }

    }
}
