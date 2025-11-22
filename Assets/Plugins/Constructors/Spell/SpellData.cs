using GameBase.Creatures;
using GameBase.Indicators;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class SpellCampData
    {
        public CampSet.Typedef Include {  get; set; }
        public CampSet.Typedef Exclude { get; set; }
    }

    public class SpellIndicatorData
    {
        public CastIndicatorType Type { get; set; }
        public float Length { get; set; }
        public float Radius { get; set; }
    }

    public class SpellData
    {
        public float MinCastAngle {  get; set; }
        public string TextureName { get; set; }
        public float Cooldown {  get; set; }
        public GameBase.Spells.Tag Tag { get; set; }

        public SpellCampData Camp { get; set; }
        public SpellIndicatorData Indicator { get; set; }
        public string ActionName { get; set; }
    }
}