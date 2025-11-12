using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;

namespace GameBase.Creatures
{
    public struct Camp : ISpellCamp, ITriggerCamp
    {
        public enum Typedef : uint
        {
            None = 0,
            Player = 1 << 0,
            Neutral = 1 << 1,
            Enermy = 1 << 2,
            ALL = 0xFFFFFFFF,
        }

        public uint value;
        public Camp(uint value)
        {
            this.value = value;
        }
        public Camp(Typedef value)
        {
            this.value = (uint)value;
        }
        public static implicit operator uint(Camp camp) => camp.value;
        public static implicit operator Typedef (Camp camp) => (Typedef)camp.value;
        public static implicit operator Camp(uint value) => new Camp(value);
        public static implicit operator Camp(Typedef value) => new Camp(value);
        public Camp And(Camp other)
        {
            return SuperEnum.And(value, other.value);
        }

        public Camp Or(Camp other)
        {
            return SuperEnum.Add(value, other.value);
        }
        ISpellCamp ISpellCamp.Or(ISpellCamp other)
        {
            return Or(other.ToUint());
        }

        ITriggerCamp ITriggerCamp.Or(ITriggerCamp other)
        {
            return Or(other.ToUint());
        }
        ISpellCamp ISpellCamp.And(ISpellCamp other)
        {
            return And(other.ToUint());
        }

        ITriggerCamp ITriggerCamp.And(ITriggerCamp other)
        {
            return And(other.ToUint());
        }

        public bool IsNone()
        {
            return value == 0;
        }

        public uint ToUint()
        {
            return value;
        }
    }

    public struct CampSet : ISpellCampSet
    {
        public enum Typedef : uint
        {
            None = 0,
            Self = 1 << 0,
            Others = 1 << 1,
            Enermy = 1 << 2,
            OthersWithoutNeutral = 1 << 3,
            ALL = 0xFFFFFFFF,
        }

        public Typedef include;
        public Typedef exclude;

        private Camp.Typedef GetCamp(Typedef set, Camp.Typedef campBase)
        {
            Camp.Typedef campRet = Camp.Typedef.None;
            if ((set & Typedef.Self) != 0)
            {
                campRet |= campBase;
            }
            if ((set & Typedef.Others) != 0)
            {
                campRet |= (~campBase);
            }
            if ((set & Typedef.OthersWithoutNeutral) != 0)
            {
                campRet |= (~(campBase | Camp.Typedef.Neutral));
            }
            if ((set & Typedef.Enermy) != 0)
            {
                campRet |= Camp.Typedef.Enermy;
            }

            return campRet;
        }

        public Camp GetCamp(Camp baseCamp)
        {
            uint campInclude = (uint)GetCamp(include, (Camp.Typedef)(baseCamp.ToUint()));
            uint campExclude = (uint)GetCamp(exclude, (Camp.Typedef)(baseCamp.ToUint()));
            return (Camp)SuperEnum.Sub(campInclude, campExclude);
        }

        ISpellCamp ISpellCampSet.GetCamp(ISpellCamp baseCamp)
        {
            uint campInclude = (uint)GetCamp(include, (Camp.Typedef)(baseCamp.ToUint()));
            uint campExclude = (uint)GetCamp(exclude, (Camp.Typedef)(baseCamp.ToUint()));
            return (Camp)SuperEnum.Sub(campInclude, campExclude);
        }
    }


    public partial class Creature
    {
        public Camp camp;
    }
}
