using System;

namespace GameBase.Tools
{
    public class SuperEnum
    {
        public static uint Add(uint e1, uint e2)
        {
            return e1 | e2;
        }

        public static uint And(uint e1, uint e2)
        {
            return e1 & e2;
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

        public static bool TryParse<T>(string s, out T e) where T : struct
        {
            if (s == null)
            {
                e = default;
                return true;
            }
            s.Replace(" ", "");
            string[] ss = s.Split('|');
            T ret = default;
            foreach (var st in ss)
            {
                T te = default;
                bool parseOK = Enum.TryParse(st, out te);
                if (!parseOK)
                {
                    e = ret;
                    return false;
                }
                else
                {
                    uint ite = Convert.ToUInt32(te);
                    uint iret = Convert.ToUInt32(ret);
                    iret += ite;
                    ret = (T)Enum.ToObject(typeof(T), iret);
                }
            }

            e = ret;
            return true;
        }
    }
}
