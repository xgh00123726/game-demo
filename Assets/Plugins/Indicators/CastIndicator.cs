using UnityEngine;

namespace GameBase.Indicators
{
    public struct IndicatorConfig
    {
        public Vector3 position;
        public Vector3 targetPosition;
        public float length;
    }
    public abstract class CastIndicator
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void Set(IndicatorConfig config);
    }
}
