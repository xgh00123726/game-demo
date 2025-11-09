namespace GameBase.Creatures
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

    public partial class Creature
    {
        public CampType camp;
    }
}
