using System.Net.Sockets;
using System.Text;

namespace SudokuRMI.client
{
    public class SudokuClient
    {
        public void Start()
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("=== MENÚ SUDOKU CLIENTE ===");
                Console.WriteLine("1. Generar Sudoku");
                Console.WriteLine("2. Mostrar Sudoku");
                Console.WriteLine("3. Insertar número");
                Console.WriteLine("4. Verificar si está completo");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                var opcion = Console.ReadLine();

                string comando = "";

                switch (opcion)
                {
                    case "1":
                        comando = "GENERAR";
                        break;
                    case "2":
                        comando = "MOSTRAR";
                        break;
                    case "3":
                        Console.Write("Fila: ");
                        int fila = int.Parse(Console.ReadLine());
                        Console.Write("Columna: ");
                        int col = int.Parse(Console.ReadLine());
                        Console.Write("Valor: ");
                        int val = int.Parse(Console.ReadLine());
                        comando = $"INSERTAR {fila} {col} {val}";
                        break;
                    case "4":
                        comando = "COMPLETO";
                        break;
                    case "0":
                        salir = true;
                        continue;
                }

                if (!string.IsNullOrEmpty(comando))
                {
                    string respuesta = EnviarComando(comando);
                    Console.WriteLine("\nRespuesta:\n" + respuesta);
                }

                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private string EnviarComando(string comando)
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(comando);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            client.Close();
            return response;
        }
    }
}
