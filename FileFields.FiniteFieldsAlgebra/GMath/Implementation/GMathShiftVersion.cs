using System;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathShiftVersion : GMathBase
    {
        #region Fields
        protected readonly int _primPoly;
        protected readonly int _ilogOffset; 
        #endregion

        #region Constructors
        public GMathShiftVersion(int dim, int primPoly, int ilogOffset)
            : base(dim)
        {
            _primPoly = primPoly;
            _ilogOffset = ilogOffset;
        } 
        #endregion

        #region GMathBase
        public override int Log(int x)
        {
            throw new NotImplementedException(string.Format("Dimension {0} is too big", _dim));
        }

        public override int ILog(int x)
        {
            throw new NotImplementedException(string.Format("Dimension {0} is too big", _dim));
        }

        protected override int MulImpl(int x, int y)
        {
            int[] scratch = new int[33];

            for (int i = 0; i < _dim; i++)
            {
                scratch[i] = y;
                if ((y & (1 << (_dim - 1))) != 0)
                {
                    y = y << 1;
                    y = (y ^ _primPoly) & _ilogOffset;
                }
                else
                {
                    y = y << 1;
                }
            }

            int prod = 0;
            for (int i = 0; i < _dim; i++)
            {
                int ind = (1 << i);
                if ((ind & x) != 0)
                {
                    int j = 1;
                    for (int k = 0; k < _dim; k++)
                    {
                        prod = prod ^ (j & scratch[i]);
                        j = (j << 1);
                    }
                }
            }
            return prod;
        }

        public override int Inv(int x)
        {
            int[] mat2 = new int[32];
            int[] inv2 = new int[32];

            for (int i = 0; i < _dim; i++)
            {
                mat2[i] = x;

                if ((x & GConstants.nw[_dim - 1]) != 0)
                {
                    x = x << 1;
                    x = (x ^ _primPoly) & _ilogOffset;
                }
                else
                {
                    x = x << 1;
                }
            }

            InvertBinaryMatrix(mat2, inv2, _dim);

            return inv2[0];
        }

        private void InvertBinaryMatrix(int[] mat, int[] inv, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                inv[i] = (int)(1 << i);
            }

            /* First -- convert into upper triangular */
            int tmp;
            int cols = rows;
            for (int i = 0; i < cols; i++)
            {
                /* Swap rows if we ave a zero i,i element.  If we can't swap, then the 
                   matrix was not invertible */

                if ((mat[i] & (1 << i)) == 0)
                {
                    int j;
                    for (j = i + 1; j < rows && (mat[j] & (1 << i)) == 0; j++) ;
                    if (j == rows)
                    {
                        throw new ArgumentException("Matrix not invertible!!\n");
                    }
                    tmp = mat[i]; mat[i] = mat[j]; mat[j] = tmp;
                    tmp = inv[i]; inv[i] = inv[j]; inv[j] = tmp;
                }

                /* Now for each j>i, add A_ji*Ai to Aj */
                for (int j = i + 1; j != rows; j++)
                {
                    if ((mat[j] & (1 << i)) != 0)
                    {
                        mat[j] ^= mat[i];
                        inv[j] ^= inv[i];
                    }
                }
            }

            /* Now the matrix is upper triangular.  Start at the top and multiply down */

            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if ((mat[j] & (1 << i)) != 0)
                    {
                        /*        mat[j] ^= mat[i]; */
                        inv[j] ^= inv[i];
                    }
                }
            }
        }

        protected override int DivImpl(int x, int y)
        {
            if (x == 0) return 0;
            int sum_j = Inv(y);
            return Mul(x, sum_j);
        } 
        #endregion
    }
}
