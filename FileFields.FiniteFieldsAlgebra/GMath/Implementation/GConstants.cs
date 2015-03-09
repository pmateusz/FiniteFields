using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFields.FiniteFieldsAlgebra.GMath.Implementation
{
    internal sealed class GConstants
    {
        internal readonly static EGMathVersion[] mult_type;
        internal readonly static int[] prim_poly;
        internal readonly static int[] nw;
        internal readonly static int[] nwm1;

        static GConstants()
        {
            unchecked
            {
                mult_type = new EGMathVersion[] 
                {
                    /*  0 */   EGMathVersion.None, 
                    /*  1 */   EGMathVersion.Table, 
                    /*  2 */   EGMathVersion.Table,
                    /*  3 */   EGMathVersion.Table,
                    /*  4 */   EGMathVersion.Table,
                    /*  5 */   EGMathVersion.Table,
                    /*  6 */   EGMathVersion.Table,
                    /*  7 */   EGMathVersion.Table,
                    /*  8 */   EGMathVersion.Table,
                    /*  9 */   EGMathVersion.Table,
                    /* 10 */   EGMathVersion.Logs,
                    /* 11 */   EGMathVersion.Logs,
                    /* 12 */   EGMathVersion.Logs,
                    /* 13 */   EGMathVersion.Logs,
                    /* 14 */   EGMathVersion.Logs,
                    /* 15 */   EGMathVersion.Logs,
                    /* 16 */   EGMathVersion.Logs,
                    /* 17 */   EGMathVersion.Logs,
                    /* 18 */   EGMathVersion.Logs,
                    /* 19 */   EGMathVersion.Logs,
                    /* 20 */   EGMathVersion.Logs,
                    /* 21 */   EGMathVersion.Logs,
                    /* 22 */   EGMathVersion.Logs,
                    /* 23 */   EGMathVersion.Shift,
                    /* 24 */   EGMathVersion.Shift,
                    /* 25 */   EGMathVersion.Shift,
                    /* 26 */   EGMathVersion.Shift,
                    /* 27 */   EGMathVersion.Shift,
                    /* 28 */   EGMathVersion.Shift,
                    /* 29 */   EGMathVersion.Shift,
                    /* 30 */   EGMathVersion.Shift,
                    /* 31 */   EGMathVersion.Shift,
                    /* 32 */   EGMathVersion.SplitW8
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
    }
}
