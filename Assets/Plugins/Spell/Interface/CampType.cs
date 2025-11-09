using GameBase.Tools;

namespace GameBase.Spells
{
    public enum CampType : uint
    {
        None = 0,
        Player = 1 << 0,
        Neutral = 1 << 1,
        Enermy = 1 << 2, 
        ALL = 0xFFFFFFFF,
    }

    public enum CampSet : uint
    {
        None = 0,
        Self = 1 << 0,
        Others = 1 << 1,
        Enermy = 1 << 2,
        OthersWithoutNeutral = 1 << 3,
        ALL = 0xFFFFFFFF,
    }

    public class CampUtil
    {
        public static CampType GetCampType(CampSet campInclude, CampSet campExlude, CampType campBase)
        {
            return (CampType)SuperEnum.Sub((uint)GetCampType(campInclude, campBase), (uint)GetCampType(campExlude, campBase));
        }

        public static CampType GetCampType(CampSet set, CampType campBase)
        {
            CampType campRet = CampType.None;
            if ((set & CampSet.Self) != 0)
            {
                campRet |= campBase;
            }
            if ((set & CampSet.Others) != 0)
            {
                campRet |= (~campBase);
            }
            if ((set & CampSet.OthersWithoutNeutral) != 0)
            {
                campRet |= (~(campBase | CampType.Neutral));
            }
            if ((set & CampSet.Enermy) != 0)
            {
                campRet |= CampType.Enermy;
            }

            return campRet;
        }
    }
}
