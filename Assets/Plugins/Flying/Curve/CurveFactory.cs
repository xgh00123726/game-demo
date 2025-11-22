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

    public class CurveData
    {
        public CurveType Type {  get; set; }
        public float Length { get; set; }
        public float Duration { get; set; }
        public float TurnSpeed { get; set; }
        public float TurnAcc {  get; set; }
    }
    public class CurveFactory
    {
        public static Curve CreateInstance(CurveData data)
        {
            if (data.Type == CurveType.None)
            {
                return null;
            }
            if (data.Type == CurveType.Linear)
            {
                return new Linear()
                {
                    Duration = data.Duration,
                };
            }
            if (data.Type == CurveType.Tracer)
            {
                return new Tracer()
                {
                    Duration = data.Duration,
                    TurnSpeed = data.TurnSpeed,
                    TurnAcc = data.TurnAcc
                };
            }
            if (data.Type == CurveType.Slower)
            {
                return new Slower()
                {
                    Duration = data.Duration,
                };
            }
            if (data.Type == CurveType.Fall)
            {
                return new Fall()
                {
                    Duration = data.Duration,
                };
            }
            if (data.Type == CurveType.Vector)
            {
                return new Vector()
                {
                    Duration = data.Duration,
                    Length = data.Length,
                };
            }

            return null;
        }
    }
}
