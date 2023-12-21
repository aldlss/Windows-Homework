using System.IO.Pipes;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await using var client = new NamedPipeClientStream(".", "aldlss", PipeDirection.InOut);
            var clientWrite = (string s) =>
            {
                Console.WriteLine($"Client: {s}");
            };
            clientWrite("Waiting");
            await client.ConnectAsync();
            clientWrite("Connected");
            var clientWriter = new StreamWriter(client);
            var clientReader = new StreamReader(client);
            clientWriter.AutoFlush = true;
            for (int i = 0; i < 10; i++)
            {
                clientWrite($"Send {i}");
                await clientWriter.WriteLineAsync(i.ToString());
            }
            clientWrite("Send exit");
            await clientWriter.WriteLineAsync("exit");
            var line = await clientReader.ReadLineAsync();
            clientWrite($"Receive {line}");
        }
    }
}