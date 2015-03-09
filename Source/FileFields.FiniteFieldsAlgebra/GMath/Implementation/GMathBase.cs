using System;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal abstract class GMathBase : IGMath
    {
        #region Fields
        protected readonly int _dim; 
        #endregion

        #region Constructors
        public GMathBase(int dim)
        {
            _dim = dim;
        } 
        #endregion

        #region IGMath
        public int Dim
        {
            get { return _dim; }
        }

        public virtual int Inv(int x)
        {
            if (x == 0)
            {
                throw new ArgumentException();
            }

            return DivImpl(1, x);
        }

        public int Add(int x, int y)
        {
            return x ^ y;
        }

        public int Sub(int x, int y)
        {
            return x ^ y;
        }

        public int Div(int x, int y)
        {
            if (y == 0)
            {
                throw new DivideByZeroException();
            }

            return DivImpl(x, y);
        }

        public int Mul(int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return 0;
            }

            return MulImpl(x, y);
        }

        public abstract int Log(int x);
        public abstract int ILog(int x);
        protected abstract int MulImpl(int x, int y);
        protected abstract int DivImpl(int x, int y);
        #endregion
    }
}
