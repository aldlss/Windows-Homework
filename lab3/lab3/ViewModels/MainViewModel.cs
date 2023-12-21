using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace lab3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
    private string _ordered = "";

    [ObservableProperty] private string _output = "";
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CallWaiterCommand))]
    [NotifyCanExecuteChangedFor(nameof(LetWaiterGoCommand))]
    private bool _waiterCome;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CallWaiterCommand))]
    [NotifyCanExecuteChangedFor(nameof(LetWaiterGoCommand))]
    [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
    private bool _isBusy;

    private bool CanOrder => !string.IsNullOrEmpty(Ordered) && !IsBusy;

    [RelayCommand(CanExecute = nameof(CanOrder))]
    private void Order()
    {
        GuestOrderEvent?.Invoke(new GuestOrderEventArgs(this, Ordered));
    }


    private bool CanCallWaiter => !WaiterCome && !IsBusy;
    [RelayCommand(CanExecute = nameof(CanCallWaiter))]
    private void CallWaiter()
    {
        Output += "叫了服务员\n";
        GuestOrderEvent += OnOrderedWithWaiter;
        WaiterCome = true;
    }

    private bool CanLetWaiterGo => WaiterCome && !IsBusy;
    [RelayCommand(CanExecute = nameof(CanLetWaiterGo))]
    private void LetWaiterGo()
    {
        Output += "服务员走了\n";
        GuestOrderEvent -= OnOrderedWithWaiter;
        WaiterCome = false;
    }

    private async void OnOrderedWithWaiter(GuestOrderEventArgs args)
    {
        IsBusy = true;
        args.Self.Output += "服务员：您点了" + args.Ordered + "\n";
        args.Self.Output += "服务员：请稍等\n服务员离开了\n";
        await Task.Delay(1000);
        args.Self.Output += "服务员回来了\n";
        args.Self.Output += "服务员：您的" + args.Ordered + "好了\n";
        LetWaiterGo();
        IsBusy = false;
    }

    private struct GuestOrderEventArgs
    {
        public readonly MainViewModel Self;
        public readonly string Ordered;
        public GuestOrderEventArgs(MainViewModel self, string ordered)
        {
            Self = self;
            Ordered = ordered;
        }
    }
    private delegate void GuestOrderEventHandler(GuestOrderEventArgs args);
    private event GuestOrderEventHandler? GuestOrderEvent;

    public MainViewModel()
    {
        GuestOrderEvent += (args => Output += "意图点单\n");
    }
}
