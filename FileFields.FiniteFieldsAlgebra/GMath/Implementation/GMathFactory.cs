using System;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    public sealed class GMathFactory : IGMathFactory
    {
        #region Fields
        private static WeakReference<int[]>[] LogTables     = new WeakReference<int[]>[33];
        private static WeakReference<int[]>[] ILogTables    = new WeakReference<int[]>[33];
        private static WeakReference<int[]>[] MultTables    = new WeakReference<int[]>[33];
        private static WeakReference<int[]>[] DivTables     = new WeakReference<int[]>[33];
        private static WeakReference<int[][]> SplitW8Tables = new WeakReference<int[][]>(null);

        private readonly GTableBuilder _tableBuilder = new GTableBuilder(); 
        #endregion

        #region Methods
        #region IGMathFactory
        public IGMath GetInstance(int w)
        {
            if (w <= 0 || w > 32)
            {
                throw new ArgumentException("Only fields of degree [1-32] are supported");
            }

            switch (GConstants.mult_type[w])
            {
                case EGMathVersion.Table:
                    int[] logTable, ilogTable, mulTable, divTable;

                    GetLogTables(w, out logTable, out ilogTable);
                    GetMultTables(w, logTable, ilogTable, out mulTable, out divTable);

                    return new GMathTableVersion(w, GConstants.nwm1[w], mulTable, divTable, logTable, ilogTable);
                case EGMathVersion.Logs:
                    GetLogTables(w, out logTable, out ilogTable);

                    return new GMathLogVersion(w, GConstants.nwm1[w], logTable, ilogTable);
                case EGMathVersion.Shift:
                    return new GMathShiftVersion(w, GConstants.prim_poly[w], GConstants.nwm1[w]);
                case EGMathVersion.SplitW8:
                    int[][] splitTables = null;
                    SplitW8Tables.TryGetTarget(out splitTables);
                    if (splitTables == null)
                    {
                        _tableBuilder.CreateSplitW8Tables(out splitTables);
                        SplitW8Tables.SetTarget(splitTables);
                    }

                    return new GMathSplitW8Version(w, GConstants.prim_poly[w], GConstants.nwm1[w], splitTables);
                default:
                    throw new ArgumentException(string.Format("Fields of degree {0} are not supported", w));
            }
        }

        public IVectorTools GetVectorTools(int w)
        {
            IGMath gmath = GetInstance(w);
            return new GMathVectorTools(gmath);
        }

        public IEquationGenerator GetGenerator(int w)
        {
            IGMath gmath = GetInstance(w);
            return new GMathNoZeroEquationGenerator(gmath);
        }

        public ISolver GetSolver(int w)
        {
            IGMath gmath = GetInstance(w);
            return new GMathSolver(gmath);
        } 
        #endregion

        private void GetLogTables(int w, out int[] logTable, out int[] ilogTable)
        {
            logTable = GetWeakReference(LogTables, w);
            ilogTable = GetWeakReference(ILogTables, w);
            if (logTable == null || ilogTable == null)
            {
                _tableBuilder.CreateLogTablesInMemory(w, out logTable, out ilogTable);
                SetWeakReference(LogTables, w, logTable);
                SetWeakReference(ILogTables, w, ilogTable);
            }
        }

        private void GetMultTables(int w, int[] logTable, int[] ilogTable, out int[] mulTable, out int[] divTable)
        {
            mulTable = GetWeakReference(MultTables, w);
            divTable = GetWeakReference(DivTables, w);
            if (mulTable == null || divTable == null)
            {
                _tableBuilder.CreateMultTablesInMemory(w, logTable, ilogTable, out mulTable, out divTable);
                SetWeakReference(MultTables, w, mulTable);
                SetWeakReference(DivTables, w, divTable);
            }
        }

        private T GetWeakReference<T>(WeakReference<T>[] weakReferenceArray, int index) where T : class
        {
            if (weakReferenceArray[index] == null)
            {
                return default(T);
            }

            T result = default(T);
            weakReferenceArray[index].TryGetTarget(out result);

            return result;
        }

        private void SetWeakReference<T>(WeakReference<T>[] weakReferenceArray, int index, T target) where T : class
        {
            if (weakReferenceArray[index] == null)
            {
                weakReferenceArray[index] = new WeakReference<T>(target);
                return;
            }

            weakReferenceArray[index].SetTarget(target);
        } 
        #endregion
    }
}
