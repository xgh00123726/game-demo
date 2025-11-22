namespace GameBase.Indicators
{
    public enum IndicatorType
    {
        Circle,
        Linear,
        Rect,
        FixedLinear,
    }

    public class CastIndicatorFactory
    {
        private static CircleIndicator _circleIndicator;
        private static LinearIndicator _linearIndicator;
        private static FixedLinearIndicator _fixedLinearIndicator;
        private static RectIndicator _rectIndicator;

        static CastIndicatorFactory()
        {
            _circleIndicator = GetCircle();
            _linearIndicator = GetLinear();
            _fixedLinearIndicator = GetFixedLinear();
            _rectIndicator = GetRect();
        }

        public static CircleIndicator GetCircle()
        {
            var e = new CircleIndicator();
            e.Indicator = IndicatorSys.Instance.NewEntity("Prefabs/Info/CircleIndicator");
            e.Indicator.TextureName = "Textures/Circle";
            e.Hide();
            return e;
        }
        public static LinearIndicator GetLinear()
        {
            var e = new LinearIndicator();
            e.Indicator = IndicatorSys.Instance.NewEntity("Prefabs/Info/RectIndicator");
            e.Indicator.TextureName = "Textures/IndicatorRectDecal";
            e.Hide();
            return e;
        }
        public static FixedLinearIndicator GetFixedLinear()
        {
            var e = new FixedLinearIndicator();
            e.Indicator = IndicatorSys.Instance.NewEntity("Prefabs/Info/RectIndicator");
            e.Indicator.TextureName = "Textures/IndicatorRectDecal";
            e.Hide();
            return e;
        }

        private static RectIndicator GetRect()
        {
            var e = new RectIndicator();
            e.Indicator = IndicatorSys.Instance.NewEntity("Prefabs/Info/RectIndicator");
            e.Indicator.TextureName = "Textures/IndicatorRectDecal";
            e.Hide();
            return e;
        }
        public static CastIndicator Get(IndicatorType type)
        {
            switch (type)
            {
                default:
                case IndicatorType.Circle:
                    return _circleIndicator;
                case IndicatorType.Linear:
                    return _linearIndicator;
                case IndicatorType.FixedLinear:
                    return _fixedLinearIndicator;
                case IndicatorType.Rect:
                    return _rectIndicator;
            }
        }
    }
}
