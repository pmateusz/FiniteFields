using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath
{
    public interface IVectorTools
    {
        int Dim { get; }

        void Swap(int[] A, int x, int y);
        void Swap(int[] A, int[] B, int from, int to);

        /// <summary>
        /// A = A - B * s
        /// </summary>
        void SubScalarMult(int[] A, int[] B, int s, int from, int to);

        /// <summary>
        /// A[x] = A[x] - A[y] * s
        /// </summary>
        void SubScalarMult(int[] A, int s, int x, int y);

        /// <returns>Sum(A * B')</returns>
        int SumMul(int[] A, int[] B, int from, int to);
    }
}
