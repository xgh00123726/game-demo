using GameBase.GCamera;
using GameBase.Indicators;
using GameBase.Math;
using GameBase.Spells;
using GameBase.Tools;
using Instance.Indicators;

namespace Instance.Spells
{
    public class PlayerPanelSpell : ViewableSpell
    {
        public enum IndicatorType
        {
            Circle,
            Rect,
            Linear,
            Sector
        }
        public PlayerPanelSpell()
        {
            RegistertoActivesDelegate += InitCastIndicator;
        }

        public IndicatorType type;
        public float indicatorLength;

        private void InitCastIndicator(Spell spell)
        {
            if (type == IndicatorType.Circle)
            {
                var indicator = IndicatorSys.Instance.NewEntity<CircleIndicator>();
                spell.indicator = indicator;
                spell.SpellReadyingDelegate = () =>
                {
                    indicator.position = CameraSys.MouseHitPosition;
                };
            }
            if (type == IndicatorType.Linear)
            {
                var indicator = IndicatorSys.Instance.NewEntity<LinearIndicator>();
                spell.indicator = indicator;
                spell.SpellReadyingDelegate = () =>
                {
                    indicator.position = spell.speller.Position;
                    indicator.Dir = CameraSys.MouseHitPosition - spell.speller.Position;
                    indicator.Length = indicatorLength;
                };
            }
        }
    }
}
