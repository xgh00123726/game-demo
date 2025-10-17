namespace GameBase.Indicators
{
    public class IndicatorFactory
    {
        public static Indicator Get(IndicatorType type)
        {
            if (type == IndicatorType.Circle)
            {
                var e = IndicatorSys.Instance.NewEntity(45);
                e.textureID = 10;
                e.obj.SetActive(false);
                return e;
            }

            return null;
        }
    }
}
