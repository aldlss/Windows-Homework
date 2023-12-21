using System.IO.Pipes;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await using var server = new NamedPipeServerStream("aldlss", PipeDirection.InOut);
            var serverWrite = (string s) =>
            {
                 Console.WriteLine($"Server: {s}");
            };
            serverWrite("Waiting");
            await server.WaitForConnectionAsync();
            serverWrite("Connected");
            var serverReader = new StreamReader(server);
            var serverWriter = new StreamWriter(server);
            while (true)
            {
                var line = await serverReader.ReadLineAsync();
                if (line is not null)
                {
                    serverWrite($"Receive {line}");
                    if (line == "exit")
                    {
                        serverWrite("Send Bye!");
                        await serverWriter.WriteLineAsync("Bye!");
                        await serverWriter.FlushAsync();
                        break;
                    }
                }
            }
        }
    }
}