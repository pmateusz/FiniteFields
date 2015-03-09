using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal sealed class GTableBuilder
    {
        private const int MaxSupportedMultTable = 13;
        private const int MaxSupportedLogTable = 30;

        /// <exception cref="ArgumentException"/>
        internal void CreateMultTablesInMemory(int w, int[] logTable, int[] ilogTable, out int[] multTable, out int[] divTable)
        {
            if (w > MaxSupportedMultTable)
            {
                throw new ArgumentException(string.Format("Field size higher than {0} are not supported", MaxSupportedMultTable));
            }

            int[] locMultTable = new int[GConstants.nw[w] * GConstants.nw[w]];
            int[] locDivTable = new int[GConstants.nw[w] * GConstants.nw[w]];

            /* Set mult/div tables for x = 0 */
            locMultTable[0] = 0;   /* y = 0 */
            locDivTable[0] = -1;
            for (int y = 1; y < GConstants.nw[w]; y++)
            {   /* y > 0 */
                locMultTable[y] = 0;
                locDivTable[y] = 0;
            }

            int j = GConstants.nw[w];
            for (int x = 1; x < GConstants.nw[w]; x++)
            {  /* x > 0 */
                locMultTable[j] = 0; /* y = 0 */
                locDivTable[j] = -1;
                j++;
                int logx = logTable[x];
                for (int y = 1; y < GConstants.nw[w]; y++)
                {  /* y > 0 */
                    locMultTable[j] = ilogTable[GConstants.nwm1[w] + logx + logTable[y]];
                    locDivTable[j] = ilogTable[GConstants.nwm1[w] + logx - logTable[y]];
                    j++;
                }
            }

            multTable = locMultTable;
            divTable = locDivTable;
        }

        /// <exception cref="ArgumentException"/>
        internal void CreateMultTablesInMemory(int w, out int[] multTable, out int[] divTable)
        {
            if (w > MaxSupportedMultTable)
            {
                throw new ArgumentException(string.Format("Field size higher than {0} are not supported",MaxSupportedMultTable));
            }

            int[] logTable;
            int[] ilogTable;

            CreateLogTablesInMemory(w, out logTable, out ilogTable);
            CreateMultTablesInMemory(w, logTable, ilogTable, out multTable, out divTable);
        }

        /// <exception cref="ArgumentException"/>
        /// <exception cref="InvalidProgramException"/>
        internal void CreateLogTablesInMemory(int w, out int[] logTable, out int[] iLogTable)
        {
            if (w > 30)
            {
                throw new ArgumentException(string.Format("Field size higher than {0} are not supported", MaxSupportedLogTable));
            }

            int[] locLogTable = new int[GConstants.nw[w]];
            int[] locILogTable = new int[GConstants.nw[w] * 3];

            for (int j = 0; j < GConstants.nw[w]; j++)
            {
                locLogTable[j] = GConstants.nwm1[w];
                locILogTable[j] = 0;
            }

            int b = 1;
            for (int j = 0; j < GConstants.nwm1[w]; j++)
            {
                if (locLogTable[b] != GConstants.nwm1[w])
                {
                    string errorMessage = string.Format("Could not build log table j={0}, b={1}, log_table[b]={2}, ilogTable[j]={3}\n",
                        j, b, locLogTable[b], locILogTable[j]);
                    throw new InvalidProgramException(errorMessage);
                }
                locLogTable[b] = j;
                locILogTable[j] = b;
                b = b << 1;
                if ((b & GConstants.nw[w]) != 0)
                {
                    b = (b ^ GConstants.prim_poly[w]) & GConstants.nwm1[w];
                }
            }

            for (int j = 0; j < GConstants.nwm1[w]; j++)
            {
                locILogTable[j + GConstants.nwm1[w]] = locILogTable[j];
                locILogTable[j + GConstants.nwm1[w] * 2] = locILogTable[j];
            }

            logTable = locLogTable;
            iLogTable = locILogTable;
        }

        internal void CreateSplitW8Tables(int[] multTable, int[] divTable, out int[][] splitTables)
        {
            int[][] locSplitTables = new int[8][];

            for (int i = 0; i < 7; i++)
            {
                locSplitTables[i] = new int[(1 << 16)];
            }

            for (int i = 0; i < 4; i += 3)
            {
                int ishift = i * 8;
                for (int j = ((i == 0) ? 0 : 1); j < 4; j++)
                {
                    int jshift = j * 8;
                    int[] table = locSplitTables[i + j];
                    for (int p1 = 0; p1 < 256; p1++)
                    {
                        int p1elt = (p1 << ishift);
                        for (int p2 = 0; p2 < 256; p2++)
                        {
                            int p2elt = (p2 << jshift);
                            table[p2] = ShiftMultiply(p1elt, p2elt, 32);
                        }
                    }
                }
            }

            splitTables = locSplitTables;
        }

        internal void CreateSplitW8Tables(out int[][] splitTables)
        {
            int[] multTable = null;
            int[] divTable = null;

            CreateMultTablesInMemory(8, out multTable, out divTable);
            CreateSplitW8Tables(multTable, divTable, out splitTables);
        }

        private static int ShiftMultiply(int x, int y, int w)
        {
            int[] scratch = new int[33];

            for (int i = 0; i < w; i++)
            {
                scratch[i] = y;
                if ((y & (1 << (w - 1))) != 0)
                {
                    y = y << 1;
                    y = (y ^ GConstants.prim_poly[w]) & GConstants.nwm1[w];
                }
                else
                {
                    y = y << 1;
                }
            }

            int prod = 0;
            for (int i = 0; i < w; i++)
            {
                int ind = (1 << i);
                if ((ind & x) != 0)
                {
                    int j = 1;
                    for (int k = 0; k < w; k++)
                    {
                        prod = prod ^ (j & scratch[i]);
                        j = (j << 1);
                    }
                }
            }
            return prod;
        }
    }
}
