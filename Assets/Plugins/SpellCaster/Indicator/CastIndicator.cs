using GameBase.Spells;

namespace GameBase.Indicators
{
    public abstract class CastIndicator
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void Set(IndicatorConfig config);
    }
}
