namespace GameBase.Projectile
{
    public class CurveFactory
    {
        public enum CurveType
        {
            None = 0,
            Liner,
            Tracer,
            Slower,
        }

        public static CurveBase CreateInstance(CurveType type, ICurveProjectile e)
        {
            if (type == CurveType.None)
            {
                return null;
            }
            if (type == CurveType.Liner)
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

            return null;
        }
    }
}
