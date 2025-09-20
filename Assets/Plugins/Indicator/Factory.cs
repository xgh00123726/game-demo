using GameBase.Indicators;

namespace Constructor.Spells.Indicators
{
    public enum Type
    {
        Circle,
        Linear,
        Rect,
        FixedLinear,
    }

    public struct IndicatorData
    {
        public float radius;
        public float length;
    }
    public class Factory
    {
        public static CircleIndicator GetCircle(IndicatorData data)
        {
            var e = new CircleIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 19;
                e.textureID = 10;
            });
            return e;
        }
        public static LinearIndicator GetLinear(IndicatorData data)
        {
            var e = new LinearIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 21;
                e.textureID = 11;
            });
            return e;
        }
        public static FixedLinearIndicator GetFixedLinear(IndicatorData data)
        {
            var e = new FixedLinearIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 21;
                e.textureID = 11;
            });
            e.Length = data.length;
            return e;
        }
        public static IInteractiveIndicator Get(Type type, IndicatorData data)
        {
            switch (type)
            {
                default:
                case Type.Circle:
                    return GetCircle(data);
                case Type.Linear:
                    return GetLinear(data);
                case Type.FixedLinear:
                    return GetFixedLinear(data);
                case Type.Rect:
                    return GetLinear(data);
            }
        }
    }
}
