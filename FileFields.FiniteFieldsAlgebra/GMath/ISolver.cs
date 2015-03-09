using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath
{
    public interface ISolver
    {
        int Dim { get; }

        /// <exception cref="GMathException"/>
        void Solve(int[][] A, int[] Y);

        /// <exception cref="GMathException"/>
        void Solve(int[][] A, int[] Y, int length);
    }
}
