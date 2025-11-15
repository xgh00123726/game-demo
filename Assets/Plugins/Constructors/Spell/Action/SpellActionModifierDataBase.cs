using GameBase.Tools;

namespace Constructor.Spells
{
    public struct SpellActionModifierData
    {
        public string textureName;
        public int rarity;

        public int castTimes;
        public int flyingNums;

        public float fireDisfuse;
        public float flyingDistance;

        public static SpellActionModifierData operator+(SpellActionModifierData a, SpellActionModifierData b)
        {
            return new SpellActionModifierData()
            {
                rarity = -1,
                textureName = null,
                castTimes = a.castTimes + b.castTimes,
                flyingNums = a.flyingNums + b.flyingNums,
                fireDisfuse = a.fireDisfuse + b.fireDisfuse,
                flyingDistance = a.flyingDistance + b.flyingDistance,
            };
        }
    }

    public class SpellActionModifierDataBase : CsvDataBase<SpellActionModifierData, SpellActionModifierDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("SpellActionModifierDataBase.csv");
    }
}
