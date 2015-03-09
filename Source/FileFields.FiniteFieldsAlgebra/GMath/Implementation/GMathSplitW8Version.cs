
namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal class GMathSplitW8Version : GMathShiftVersion
    {
        #region Fields
        protected readonly int[][] _splitW8Tables; 
        #endregion

        #region Constructors
        public GMathSplitW8Version(int dim, int primPoly, int ilogOffset, int[][] splitW8Tables)
            : base(dim, primPoly, ilogOffset)
        {
            _splitW8Tables = splitW8Tables;
        } 
        #endregion

        #region GMathBase
        protected override int MulImpl(int x, int y)
        {
            int acc = 0;
            int i8 = 0;
            for (int i = 0; i < 4; i++)
            {
                int a = (((x >> i8) & 255) << 8);
                int j8 = 0;
                for (int j = 0; j < 4; j++)
                {
                    int b = (((int)y >> j8) & 255);
                    acc ^= (int)_splitW8Tables[i + j][a | b];
                    j8 += 8;
                }
                i8 += 8;
            }
            return (int)acc;
        } 
        #endregion
    }
}
