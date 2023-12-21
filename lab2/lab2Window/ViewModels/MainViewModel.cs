using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reactive;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ReactiveUI;

namespace lab2Window.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _output = string.Empty;
    public string Output
    {
        get => _output;
        set => this.RaiseAndSetIfChanged(ref _output, value);
    }

    public ReactiveCommand<Unit, Unit> SyncCommand { get; }
    public ReactiveCommand<Unit, Unit> AsyncCommand { get; }
    public ReactiveCommand<Unit, Unit> ServerCommand { get; }
    public ReactiveCommand<Unit, Unit> ClientCommand { get; }

    public MainViewModel()
    {
        SyncCommand = ReactiveCommand.Create(SyncReadIp);
        AsyncCommand = ReactiveCommand.CreateFromTask(AsyncReadIp);
        ServerCommand = ReactiveCommand.CreateFromTask(CreateServer);
        ClientCommand = ReactiveCommand.CreateFromTask(CreateClient);
    }

    private void SyncReadIp()
    {
        Output = "";
        using Process process = new();
        process.StartInfo.FileName = "pwsh";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        StreamWriter w = process.StandardInput;
        for (int i = 0; i < 1; i++)
        {
            w.WriteLine("ipconfig");
        }
        w.WriteLine("exit");
        Output += process.StandardOutput.ReadToEnd();
        process.Close();
    }

    private async Task AsyncReadIp()
    {
        Output = "";
        using Process process = new();
        process.StartInfo.FileName = "pwsh";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data is not null)
            {
                Output += $"{args.Data}\n";
            }
        };
        process.Start();
        StreamWriter w = process.StandardInput;
        process.BeginOutputReadLine();
        for (int i = 0; i < 1; i++)
        {
            await w.WriteLineAsync("ipconfig");
        }
        await w.WriteLineAsync("exit");
        await process.WaitForExitAsync();
    }

    private async Task CreateClient()
    {
        Output = "";
        using Process process = new();
        process.StartInfo.FileName = "./Client\\bin\\Debug\\net7.0\\Client.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data is not null)
            {
                Output += $"{args.Data}\n";
            }
        };
        process.Start();
        process.BeginOutputReadLine();
        await process.WaitForExitAsync();
    }

    private async Task CreateServer()
    {
        Output = "";
        using Process process = new();
        process.StartInfo.FileName = "./Server\\bin\\Debug\\net7.0\\Server.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.OutputDataReceived += (_, args) =>
        {
            if (args.Data is not null)
            {
                Output += $"{args.Data}\n";
            }
        };
        process.Start();
        process.BeginOutputReadLine();
        await process.WaitForExitAsync();
    }
}
