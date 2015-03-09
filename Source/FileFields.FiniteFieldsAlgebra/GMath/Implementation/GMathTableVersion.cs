
namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathTableVersion : GMathLogVersion
    {
        #region Fields
        protected readonly int[] _divTable;
        protected readonly int[] _mulTable; 
        #endregion

        #region Constructors
        public GMathTableVersion(int dim, int ilogOffset, int[] mulTable, int[] divTable, int[] logTable, int[] ilogTable)
            : base(dim, ilogOffset, logTable, ilogTable)
        {
            _mulTable = mulTable;
            _divTable = divTable;
        } 
        #endregion

        #region GMathBase
        protected override int MulImpl(int x, int y)
        {
            return _mulTable[(x << _dim) | y];
        }

        protected override int DivImpl(int x, int y)
        {
            return _divTable[(x << _dim) | y];
        } 
        #endregion
    }
}
