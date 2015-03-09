using FileFields.FiniteFieldsAlgebra.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathSolver : ISolver
    {
        private readonly IGMath _gmath;
        private readonly IVectorTools _vectorTools;

        public GMathSolver(IGMath gmath)
        {
            _gmath = gmath;
            _vectorTools = new GMathVectorTools(_gmath);
        }

        public int Dim { get { return _gmath.Dim; } }

        public void Solve(int[][] A, int[] Y)
        {
            if(Y == null)
            {
                throw new ArgumentNullException("Y");
            }

            Solve(A, Y, Y.Length);
        }

        public void Solve(int[][] A, int[] Y, int length)
        {
            CheckArguments(A, Y, length);

            for (int i = 0; i < length; i++)
            {
                if (A[i][i] == 0)
                {
                    Rearrange(A, Y, i, length);
                }

                for (int row = i + 1; row < length; row++)
                {
                    if (A[row][i] != 0)
                    {
                        int factor = _gmath.Div(A[row][i], A[i][i]);
                        _vectorTools.SubScalarMult(A[row], A[i], factor, i, length);
                        _vectorTools.SubScalarMult(Y, factor, row, i);
                    }
                }
            }

            for (int i = length - 1; i >= 0; --i)
            {
                if (A[i][i] == 0)
                {
                    throw new GMathException(ExceptionStrings.SystemOfEquationsNotLinearyIndepenedent);
                }

                int v = _vectorTools.SumMul(A[i], Y, i + 1, length);
                Y[i] = _gmath.Sub(Y[i], v);

                Y[i] = _gmath.Div(Y[i], A[i][i]); 
            }
        }

        private void Rearrange(int[][] A, int[] Y, int index, int length)
        {
            for (int row = index + 1; row < length; row++)
            {
                if (A[row][index] != 0)
                {
                    _vectorTools.Swap(A[index], A[row], index, length);
                    _vectorTools.Swap(Y, index, row);
                    break;
                }
            }

            if (A[index][index] == 0)
            {
                throw new GMathException(ExceptionStrings.SystemOfEquationsNotLinearyIndepenedent);
            }
        }

        private void CheckArguments(int[][] A, int[] Y, int length)
        {
            if (Y == null)
            {
                throw new ArgumentNullException("Y");
            }

            if (Y.Length < length)
            {
                throw new ArgumentException("Y");
            }

            if (A == null)
            {
                throw new ArgumentNullException("A");
            }

            foreach (int[] row in A)
            {
                if (row == null || row.Length < length)
                {
                    throw new ArgumentException("A");
                }
            }
        }
    }
}
