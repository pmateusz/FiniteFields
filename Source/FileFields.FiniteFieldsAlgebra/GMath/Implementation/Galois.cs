using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    public sealed class Galois
    {
        private const string GaloisTablesDirectory = "GaloisTables";

        private const int NONE    = 10;
        private const int TABLE   = 11;
        private const int SHIFT   = 12;
        private const int LOGS    = 13;
        private const int SPLITW8 = 14;

        private static int[] prim_poly;
        private static int[] mult_type;
        private static int[] nw;
        private static int[] nwm1;

        static Galois()
        {
            unchecked
            {
                mult_type = new int[] 
                {
                    /*  0 */   NONE, 
                    /*  1 */   TABLE, 
                    /*  2 */   TABLE,
                    /*  3 */   TABLE,
                    /*  4 */   TABLE,
                    /*  5 */   TABLE,
                    /*  6 */   TABLE,
                    /*  7 */   TABLE,
                    /*  8 */   TABLE,
                    /*  9 */   TABLE,
                    /* 10 */   LOGS,
                    /* 11 */   LOGS,
                    /* 12 */   LOGS,
                    /* 13 */   LOGS,
                    /* 14 */   LOGS,
                    /* 15 */   LOGS,
                    /* 16 */   LOGS,
                    /* 17 */   LOGS,
                    /* 18 */   LOGS,
                    /* 19 */   LOGS,
                    /* 20 */   LOGS,
                    /* 21 */   LOGS,
                    /* 22 */   LOGS,
                    /* 23 */   SHIFT,
                    /* 24 */   SHIFT,
                    /* 25 */   SHIFT,
                    /* 26 */   SHIFT,
                    /* 27 */   SHIFT,
                    /* 28 */   SHIFT,
                    /* 29 */   SHIFT,
                    /* 30 */   SHIFT,
                    /* 31 */   SHIFT,
                    /* 32 */   SPLITW8
                };

                nwm1 = new int[]
                {
                    0, (1 << 1)-1, (1 << 2)-1, (1 << 3)-1, (1 << 4)-1, 
                    (1 << 5)-1, (1 << 6)-1, (1 << 7)-1, (1 << 8)-1, (1 << 9)-1, (1 << 10)-1,
                    (1 << 11)-1, (1 << 12)-1, (1 << 13)-1, (1 << 14)-1, (1 << 15)-1, (1 << 16)-1,
                    (1 << 17)-1, (1 << 18)-1, (1 << 19)-1, (1 << 20)-1, (1 << 21)-1, (1 << 22)-1,
                    (1 << 23)-1, (1 << 24)-1, (1 << 25)-1, (1 << 26)-1, (1 << 27)-1, (1 << 28)-1,
                    (1 << 29)-1, (1 << 30)-1, 0x7fffffff, (int) 0xffffffff
                };

                nw = new int[]
                {
                     0, (1 << 1), (1 << 2), (1 << 3), (1 << 4), 
                    (1 << 5), (1 << 6), (1 << 7), (1 << 8), (1 << 9), (1 << 10),
                    (1 << 11), (1 << 12), (1 << 13), (1 << 14), (1 << 15), (1 << 16),
                    (1 << 17), (1 << 18), (1 << 19), (1 << 20), (1 << 21), (1 << 22),
                    (1 << 23), (1 << 24), (1 << 25), (1 << 26), (1 << 27), (1 << 28),
                    (1 << 29), (1 << 30), (int) (1 << 31), (int) 0xffffffff 
                };

                prim_poly = new int[] 
                {
                    /* 0 */     0, 
                    /*  1 */    1, 
                    /*  2 */    7,
                    /*  3 */    11,
                    /*  4 */    19,
                    /*  5 */    37,
                    /*  6 */    67,
                    /*  7 */    137,
                    /*  8 */    285,
                    /*  9 */    529,
                    /* 10 */    1033,
                    /* 11 */    2053,
                    /* 12 */    4179,
                    /* 13 */    8219,
                    /* 14 */    17475,
                    /* 15 */    32771,
                    /* 16 */    69643,
                    /* 17 */    131081,
                    /* 18 */    262273,
                    /* 19 */    524327,
                    /* 20 */    1048585,
                    /* 21 */    2097157,
                    /* 22 */    4194307,
                    /* 23 */    8388641,
                    /* 24 */    16777351,
                    /* 25 */    33554441,
                    /* 26 */    67108935,
                    /* 27 */    134217767,
                    /* 28 */    268435465,
                    /* 29 */    536870917,     /*Ponizej jest w oct
                    /* 30 */    1082130439,    /*Really  010040000007*/
                    /* 31 */    -2147483639,   /*Really  020000000011*/   
                    /* 32 */    4194311        /* Really 40020000007, but we're omitting the high order bit */
                };
            }
        }

        private static int[][] LogTables = new int[][]
        {
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null
        };

        private static int[][] ILogTables = new int[][]
        {
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null
        };

        private static int[][] MultTables = new int[][]
        {
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null
        };

        private static int[][] DivTables = new int[][]
        {
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null
        };

        private static int[][] SplitW8Tables = new int[][] { null, null, null, null, null, null, null };

        public static void CreateMultTablesInMemory(int w, out int[] multTable, out int[] divTable)
        {
            if (w >= 14)
            {
                throw new ArgumentException("Field size higher than 14 are not supported");
            }

            int[] locMultTable = new int[nw[w] * nw[w]];
            int[] locDivTable = new int[nw[w] * nw[w]];

            int[] logTable;
            int[] ilogTable;

            CreateLogTablesInMemory(w, out logTable, out ilogTable);

            /* Set mult/div tables for x = 0 */
            locMultTable[0] = 0;   /* y = 0 */
            locDivTable[0] = -1;
            for (int y = 1; y < nw[w]; y++)
            {   /* y > 0 */
                locMultTable[y] = 0;
                locDivTable[y] = 0;
            }

            int j = nw[w];
            for (int x = 1; x < nw[w]; x++)
            {  /* x > 0 */
                locMultTable[j] = 0; /* y = 0 */
                locDivTable[j] = -1;
                j++;
                int logx = logTable[x];
                for (int y = 1; y < nw[w]; y++)
                {  /* y > 0 */
                    locMultTable[j] = ilogTable[nwm1[w] + logx + logTable[y]];
                    locDivTable[j] = ilogTable[nwm1[w] + logx - logTable[y]];
                    j++;
                }
            }

            multTable = locMultTable;
            divTable = locDivTable;
        }

        public static void CreateLogTablesInMemory(int w, out int[] logTable, out int[] iLogTable)
        {
            if (w > 30)
            {
                throw new ArgumentException("Field size higher than 30 are not supported");
            }

            int[] locLogTable = new int[nw[w]];
            int[] locILogTable = new int[nw[w] * 3];

            for (int j = 0; j < nw[w]; j++)
            {
                locLogTable[j] = nwm1[w];
                locILogTable[j] = 0;
            }

            int b = 1;
            for (int j = 0; j < nwm1[w]; j++)
            {
                if (locLogTable[b] != nwm1[w])
                {
                    string errorMessage = string.Format("Could not build log table j={0}, b={1}, log_table[b]={2}, ilogTable[j]={3}\n",
                        j, b, locLogTable[b], locILogTable[j]);
                    throw new InvalidProgramException(errorMessage);
                }
                locLogTable[b] = j;
                locILogTable[j] = b;
                b = b << 1;
                if ((b & nw[w]) != 0)
                {
                    b = (b ^ prim_poly[w]) & nwm1[w];
                }
            }

            for (int j = 0; j < nwm1[w]; j++)
            {
                locILogTable[j + nwm1[w]] = locILogTable[j];
                locILogTable[j + nwm1[w] * 2] = locILogTable[j];
            }

            logTable = locLogTable;
            iLogTable = locILogTable;
        }

        public static int Multiply(int x, int y, int w)
        {
            if (x == 0 || y == 0) return 0;

            if (mult_type[w] == TABLE)
            {
                if (MultTables[w] == null)
                {
                    CreateMultTables(w);
                }
                return MultTables[w][(x << w) | y];
            }
            else if (mult_type[w] == LOGS)
            {
                if (LogTables[w] == null)
                {
                    CreateLogTables(w);
                }
                int sum_j = LogTables[w][x] + LogTables[w][y];
                return ILogTables[w][nwm1[w] + sum_j];
            }
            else if (mult_type[w] == SPLITW8)
            {
                if (SplitW8Tables[0] == null)
                {
                    CreateSplitTables();
                }
                return SplitW8Multiply(x, y);
            }
            else if (mult_type[w] == SHIFT)
            {
                return ShiftMultiply(x, y, w);
            }

            throw new NotImplementedException(string.Format("No implementation for w={0}\n", w));
        }

        private static void CreateSplitTables()
        {
            try
            {
                CreateSplitW8Tables(out SplitW8Tables);
            }
            catch (InvalidProgramException e)
            {
                throw new InvalidProgramException("Cannot make log split_w8_tables\n", e);
            }
        }

        private static void CreateMultTables(int w)
        {
            try
            {
                CreateMultTablesInMemory(w, out MultTables[w], out DivTables[w]);
            }
            catch (InvalidProgramException e)
            {
                string errorMessage = string.Format("Cannot make multiplication tables for w={0}\n", w);
                throw new InvalidProgramException(errorMessage, e);
            }
        }

        private static void CreateLogTables(int w)
        {
            try
            {
                CreateLogTablesInMemory(w, out LogTables[w], out ILogTables[w]);
            }
            catch (InvalidProgramException e)
            {
                string errorMessage = string.Format("Cannot make log tables for w={0}\n", w);
                throw new InvalidProgramException(errorMessage, e);
            }
        }

        private static void CreateSplitW8Tables(out int[][] splitTables)
        {
            if (MultTables[8] == null)
            {
                CreateMultTablesInMemory(8, out MultTables[8], out DivTables[8]); 
            }

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

        public static int Divide(int a, int b, int w)
        {
            if (mult_type[w] == TABLE)
            {
                if (DivTables[w] == null)
                {
                    CreateMultTables(w);
                }
                return DivTables[w][(a << w) | b];
            }
            else if (mult_type[w] == LOGS)
            {
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                if (a == 0) return 0;
                if (LogTables[w] == null)
                {
                    CreateLogTables(w);
                }

                int sum_j =  LogTables[w][a] - LogTables[w][b];
                return ILogTables[w][nwm1[w] + sum_j];
            }
            else
            {
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                if (a == 0) return 0;
                int sum_j = Inverse(b, w);
                return Multiply(a, sum_j, w);
            }
        }

        public static int Inverse(int y, int w)
        {
            if (y == 0)
            {
                throw new ArgumentException();
            }

            if (mult_type[w] == SHIFT || mult_type[w] == SPLITW8) return ShiftInverse(y, w);
            return Divide(1, y, w);
        }

        private static int ShiftInverse(int y, int w)
        {
          int[] mat2 = new int[32];
          int[] inv2 = new int[32];
 
          for (int i = 0; i < w; i++)
          {
            mat2[i] = y;

            if ((y & nw[w-1]) != 0) {
              y = y << 1;
              y = (y ^ prim_poly[w]) & nwm1[w];
            } else {
              y = y << 1;
            }
          }

          InvertBinaryMatrix(mat2, inv2, w);

          return inv2[0]; 
        }

        private static void InvertBinaryMatrix(int[] mat, int[] inv, int rows)
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
                        throw new ArgumentException("galois_invert_matrix: Matrix not invertible!!\n");
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

        private static int ShiftMultiply(int x, int y, int w)
        {
            int[] scratch = new int[33];

            for (int i = 0; i < w; i++)
            {
                scratch[i] = y;
                if ((y & (1 << (w - 1))) != 0)
                {
                    y = y << 1;
                    y = (y ^ prim_poly[w]) & nwm1[w];
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

        private static int SplitW8Multiply(int x, int y)
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
                    acc ^= (int) SplitW8Tables[i + j][a | b];
                    j8 += 8;
                }
                i8 += 8;
            }
            return (int) acc;
        }
    }
}