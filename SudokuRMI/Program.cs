
using SudokuRMI.client;
using SudokuRMI.server;

Console.WriteLine("¿Quieres ejecutar como (S)ervidor o (C)liente?");
string modo = Console.ReadLine().ToUpper();

if (modo == "S")
{
    SudokuServer server = new SudokuServer();
    server.Start();
}
else if (modo == "C")
{
    SudokuClient client = new SudokuClient();
    client.Start();
}