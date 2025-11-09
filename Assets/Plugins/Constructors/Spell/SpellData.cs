using GameBase.Indicators;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class SpellCampData
    {
        public CampSet include;
        public CampSet exclude;
        public CampType fixedType;
    }

    public class SpellIndicatorData
    {
        public CastIndicatorType type;
        public float length;
        public float radius;
    }

    public class SpellData
    {
        public float minCastAngle;
        public string textureName;
        public float cooldown;
        public GameBase.Spells.Tag tag;

        public SpellCampData camp;
        public SpellIndicatorData indicator;
        public SpellActionData actionData;
    }
}