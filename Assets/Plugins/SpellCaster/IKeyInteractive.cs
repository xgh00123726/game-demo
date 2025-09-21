using GameBase.Indicators;

namespace GameBase.Spells
{
    public interface IKeyInteractive
    {
        IndicatorType IndicatorType { get; }
        float Length { get; }
        float Radius { get; }
        bool Invoke();
    }
}
