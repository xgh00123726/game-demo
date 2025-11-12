namespace GameBase.Flyings
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

    public struct CurveData
    {
        public CurveType type;
        public float length;
        public float duration;
        public float turnSpeed;
        public float turnAcc;
    }
    public class CurveFactory
    {
        public static Curve CreateInstance(CurveData data)
        {
            if (data.type == CurveType.None)
            {
                return null;
            }
            if (data.type == CurveType.Linear)
            {
                return new Linear()
                {
                    duration = data.duration,
                };
            }
            if (data.type == CurveType.Tracer)
            {
                return new Tracer()
                {
                    duration = data.duration,
                    turnSpeed = data.turnSpeed,
                    turnAcc = data.turnAcc
                };
            }
            if (data.type == CurveType.Slower)
            {
                return new Slower()
                {
                    duration = data.duration,
                };
            }
            if (data.type == CurveType.Fall)
            {
                return new Fall()
                {
                    duration = data.duration,
                };
            }
            if (data.type == CurveType.Vector)
            {
                return new Vector()
                {
                    duration = data.duration,
                    length = data.length,
                };
            }

            return null;
        }
    }
}
