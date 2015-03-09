
namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathLogVersion : GMathBase
    {
        #region Fields
        protected readonly int[] _ilogTable;
        protected readonly int[] _logTable;
        protected readonly int _ilogOffset; 
        #endregion

        #region Constructors
        public GMathLogVersion(int dim, int ilogOffset, int[] logTable, int[] ilogTable)
            : base(dim)
        {
            _ilogOffset = ilogOffset;
            _logTable = logTable;
            _ilogTable = ilogTable;
        } 
        #endregion

        #region GMathBase
        public override int Log(int x)
        {
            return _logTable[x];
        }

        public override int ILog(int x)
        {
            return _ilogTable[x];
        }

        protected override int MulImpl(int x, int y)
        {
            int sum_j = _logTable[x] + _logTable[y];
            int index = _ilogOffset + sum_j;
            int v = _ilogTable[index];
            return v;
        }

        protected override int DivImpl(int x, int y)
        {
            if (x == 0)
            {
                return 0;
            }
            int sum_j = _logTable[x] - _logTable[y];
            return _ilogTable[_ilogOffset + sum_j];
        } 
        #endregion
    }
}
