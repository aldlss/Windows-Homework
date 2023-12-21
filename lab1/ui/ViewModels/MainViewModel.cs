using System;
using System.Reactive;
using ReactiveUI;
using COMFramework;

namespace ui.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _a = "", _b = "", _result = "";

    public string A
    {
        get => _a;
        set => this.RaiseAndSetIfChanged(ref _a, value);
    }

    public string B
    {
        get => _b;
        set => this.RaiseAndSetIfChanged(ref _b, value);
    }

    public string Result
    {
        get => _result;
        set => this.RaiseAndSetIfChanged(ref _result, value);
    }

    public ReactiveCommand<Unit, Unit> PlusCommand { get; }
    public ReactiveCommand<Unit, Unit> MinusCommand { get; }
    public ReactiveCommand<Unit, Unit> MultiplyCommand { get; }

    public MainViewModel()
    {
        PlusCommand = ReactiveCommand.Create(Plus);
        MinusCommand = ReactiveCommand.Create(Minus);
        MultiplyCommand = ReactiveCommand.Create(Multiply);
    }

    private void Plus()
    {
        if (int.TryParse(A, out var a) && int.TryParse(B, out int b))
        {
            var aldlssCom = CreateAldlssCom();
            if (aldlssCom != null)
            {
                Result = aldlssCom.Plus(a, b).ToString();
            }
        }
    }

    public void Minus()
    {
        if (int.TryParse(A, out var a) && int.TryParse(B, out int b))
        {
            var aldlssCom = CreateAldlssCom();
            if (aldlssCom != null)
            {
                Result = aldlssCom.Minus(a, b).ToString();
            }
        }
    }

    public void Multiply()
    {
        if (int.TryParse(A, out var a) && int.TryParse(B, out int b))
        {
            var aldlssCom = CreateAldlssCom();
            if (aldlssCom != null)
            {
                Result = aldlssCom.Multiply(a, b).ToString();
            }
        }
    }


    private static Class1.IAldlssCom? CreateAldlssCom()
    {
        Class1.IAldlssCom iAldlssCom = null;
        try
        {
            Guid guid = new Guid("37E1EAD4-354C-4DCD-8320-7E030F658CE9");
            Type type = Type.GetTypeFromCLSID(guid);
            object aldlssCom = Activator.CreateInstance(type);
            iAldlssCom = aldlssCom as Class1.IAldlssCom;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return iAldlssCom;
    }
}
