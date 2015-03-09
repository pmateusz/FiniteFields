using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathVectorTools : IVectorTools
    {
        private readonly IGMath _gmath;

        public GMathVectorTools(IGMath gMath)
        {
            _gmath = gMath;
        }

        public int Dim { get { return _gmath.Dim; } }

        public void Swap(int[] A, int x, int y)
        {
            Debug.Assert(x < A.Length);
            Debug.Assert(y < A.Length);

            int swap = A[x];
            A[x] = A[y];
            A[y] = swap;
        }

        public void Swap(int[] A, int[] B, int from, int to)
        {
            Debug.Assert(from <= to);
            Debug.Assert(to <= A.Length);
            Debug.Assert(to <= B.Length);

            int swap;
            for (int col = from; col < to; col++)
            {
                swap = A[col];
                A[col] = B[col];
                B[col] = swap;
            }
        }

        /// <summary>
        /// A = A - B * s
        /// </summary>
        public void SubScalarMult(int[] A, int[] B, int s, int from, int to)
        {
            Debug.Assert(from <= to);
            Debug.Assert(to <= A.Length);
            Debug.Assert(to <= B.Length);

            int v;
            for (int i = from; i < to; i++)
            {
                v = _gmath.Mul(B[i], s);
                A[i] = _gmath.Sub(A[i], v);
            }
        }

        /// <summary>
        /// A[x] = A[x] - A[y] * s
        /// </summary>
        public void SubScalarMult(int[] A, int s, int x, int y)
        {
            Debug.Assert(x < A.Length);
            Debug.Assert(y < A.Length);

            int v = _gmath.Mul(A[y], s);
            A[x] = _gmath.Sub(A[x], v);
        }

        public int SumMul(int[] A, int[] B, int from, int to)
        {
            Debug.Assert(from <= to);
            Debug.Assert(to <= A.Length);
            Debug.Assert(to <= B.Length);

            int acc = 0;
            for (int i = from; i < to; i++)
            {
                int mul = _gmath.Mul(A[i], B[i]);
                acc = _gmath.Add(acc, mul);
            }

            return acc;
        }
    }
}
