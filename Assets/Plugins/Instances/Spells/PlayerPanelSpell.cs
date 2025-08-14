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
        private Indicator _indicator;
        public PlayerPanelSpell()
        {
            RegistertoActivesDelegate += InitCastIndicator;
        }

        private void InitCastIndicator(Spell spell)
        {
            if (spell.shape.Exist)
            {
                if (spell.shape.Get() is GMath.Circle)
                {
                    spell.SpellToReadyDelegate = () =>
                    {
                        _indicator = IndicatorSys.Instance.NewEntity<CircleIndicator>();
                    };
                    spell.SpellReadyingDelegate = () =>
                    {
                        _indicator.position = CameraSys.MouseHitPosition;
                    };
                    spell.SpellExitReadyDelegate = () =>
                    {
                        IndicatorSys.Instance.RemoveIndicator(_indicator);
                    };
                }

                if (spell.shape.Get() is GMath.Rect2D)
                {
                    spell.SpellToReadyDelegate = () =>
                    {
                        _indicator = IndicatorSys.Instance.NewEntity<RectIndicator>();
                    };
                    spell.SpellReadyingDelegate = () =>
                    {
                        _indicator.position = CameraSys.MouseHitPosition;
                    };
                    spell.SpellExitReadyDelegate = () =>
                    {
                        IndicatorSys.Instance.RemoveIndicator(_indicator);
                    };
                }
            }
        }
    }
}
