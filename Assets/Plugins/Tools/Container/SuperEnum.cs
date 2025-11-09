using System;

namespace GameBase.Tools
{
    public class SuperEnum
    {
        public static uint Add(uint e1, uint e2)
        {
            return e1 | e2;
        }

        public static uint Sub(uint e1, uint e2)
        {
            return (e1 ^ e2) & e1;
        }

        public static bool PartOverlap(uint e1, uint e2)
        {
            return (e1 & e2) != 0;
        }

        public static bool NoOverlap(uint e1, uint e2)
        {
            return (e1 & e2) == 0;
        }
    }
}
