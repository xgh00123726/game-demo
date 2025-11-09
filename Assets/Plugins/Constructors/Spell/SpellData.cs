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

    public class SpellData
    {
        public float minCastAngle;
        public string textureName;
        public float cooldown; 
        public CastIndicatorType type;
        public float length;
        public float radius;
        public GameBase.Spells.Tag tag;

        public SpellCampData camp;
        public SpellActionData actionData;
    }
}