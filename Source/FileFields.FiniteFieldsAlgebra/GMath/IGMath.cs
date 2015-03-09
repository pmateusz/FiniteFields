using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath
{
    public interface IGMath
    {
        int Dim { get; }

        int Inv(int x);
        int Log(int x);
        int ILog(int x);

        int Add(int x, int y);
        int Sub(int x, int y);
        int Div(int x, int y);
        int Mul(int x, int y);
    }
}
