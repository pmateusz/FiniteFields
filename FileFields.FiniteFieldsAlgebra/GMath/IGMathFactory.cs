using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath
{
    public interface IGMathFactory
    {
        IGMath GetInstance(int w);
        ISolver GetSolver(int w);
        IEquationGenerator GetGenerator(int w);
        IVectorTools GetVectorTools(int w);
    }
}
