namespace GameBase.Indicators
{
    public enum IndicatorType
    {
        Circle,
        Linear,
        Rect,
        FixedLinear,
    }

    public class Factory
    {
        private static CircleIndicator _circleIndicator;
        private static LinearIndicator _linearIndicator;
        private static FixedLinearIndicator _fixedLinearIndicator;
        private static RectIndicator _rectIndicator;

        static Factory()
        {
            _circleIndicator = GetCircle();
            _linearIndicator = GetLinear();
            _fixedLinearIndicator = GetFixedLinear();
            _rectIndicator = GetRect();
        }

        public static CircleIndicator GetCircle()
        {
            var e = new CircleIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 19;
                e.textureID = 10;
            });
            return e;
        }
        public static LinearIndicator GetLinear()
        {
            var e = new LinearIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 21;
                e.textureID = 11;
            });
            return e;
        }
        public static FixedLinearIndicator GetFixedLinear()
        {
            var e = new FixedLinearIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 21;
                e.textureID = 11;
            });
            return e;
        }

        private static RectIndicator GetRect()
        {
            var e = new RectIndicator();
            e.indicator = IndicatorSys.Instance.NewEntity((Indicator e) =>
            {
                e.ObjID = 21;
                e.textureID = 11;
            });
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
