using UnityEngine;

namespace GameBase.Indicators
{
    public interface IIndicatorOwner
    {
        Vector3 ShowPosition { get; }
        bool IndicatorVisble { get; }
        Vector3 Size { get; }
    }
}
