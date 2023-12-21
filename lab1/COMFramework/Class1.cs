using System.ComponentModel;
using System.Runtime.InteropServices;

namespace COMFramework
{
    public class Class1
    {
        [ComVisible(true)]
        [Guid("FF392409-0C0F-4E4E-8E18-1AD52AA377D4")]
        public interface IAldlssCom
        {
            int Plus(int a, int b);
            int Minus(int a, int b);
            int Multiply(int a, int b);
        }

        [ComVisible(true)]
        [Guid("37E1EAD4-354C-4DCD-8320-7E030F658CE9")]
        [ClassInterface(ClassInterfaceType.None)]
        [Description("Aldlss COM 组件，简单加减乘")]
        public class AldlssCom : IAldlssCom
        {
            public int Plus(int a, int b)
            {
                return a + b;
            }

            public int Minus(int a, int b)
            {
                return a - b;
            }

            public int Multiply(int a, int b)
            {
                return a * b;
            }
        }
    }
}
