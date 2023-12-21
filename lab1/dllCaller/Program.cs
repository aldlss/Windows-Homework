// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

Console.WriteLine("Call Cpp Dll");
CallCppDll();
Console.WriteLine("Call Csharp Dll");
CallCsharpDll();
//Console.WriteLine("www");
//x86
//var a = CreateAldlssCom();
//Console.WriteLine(a?.Plus(1, 2));

void CallCppDll()
{
    [DllImport("dllCpp.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern int plus(int a, int b);

    [DllImport("dllCpp.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern int minus(int a, int b);

    [DllImport("dllCpp.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern int multiply(int a, int b);

    try
    {
        Console.WriteLine("plus(1, 2) = " + plus(2, 5));
        Console.WriteLine("minus(1, 2) = " + minus(9, 23));
        Console.WriteLine("multiply(1, 2) = " + multiply(33, 111));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

void CallCsharpDll()
{
    try
    {
        Console.WriteLine("Plus(1, 2) = " + DllCsharp.Class1.Plus(2, 5));
        Console.WriteLine("Minus(1, 2) = " + DllCsharp.Class1.Minus(9, 23));
        Console.WriteLine("Multiply(1, 2) = " + DllCsharp.Class1.Multiply(33, 111));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

COMFramework.Class1.IAldlssCom? CreateAldlssCom()
{
    COMFramework.Class1.IAldlssCom iAldlssCom = null;
    try
    {
        Guid guid = new Guid("37E1EAD4-354C-4DCD-8320-7E030F658CE9");
        Type type = Type.GetTypeFromProgID("COMFramework.Class1+AldlssCom");
        object aldlssCom = Activator.CreateInstance(type);
        iAldlssCom = aldlssCom as COMFramework.Class1.IAldlssCom;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    return iAldlssCom;
}