using GameBase.Creatures;
using GameBase.Indicators;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class SpellCampData
    {
        public CampSet.Typedef include;
        public CampSet.Typedef exclude;
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
        public string actionName;
    }
}