namespace GameBase.Flyings
{
    public class CurveFactory
    {
        public enum CurveType
        {
            None = 0,
            Linear,
            Tracer,
            Slower,
            Fall,
            Vector,
        }

        public static CurveBase CreateInstance(CurveType type, ICurveable e)
        {
            if (type == CurveType.None)
            {
                return null;
            }
            if (type == CurveType.Linear)
            {
                return new Linear(e);
            }
            if (type == CurveType.Tracer)
            {
                return new Tracer(e);
            }
            if (type == CurveType.Slower)
            {
                return new Slower(e);
            }
            if (type == CurveType.Fall)
            {
                return new Fall(e);
            }
            if (type == CurveType.Vector)
            {
                return new Vector(e);
            }

            return null;
        }
    }
}
