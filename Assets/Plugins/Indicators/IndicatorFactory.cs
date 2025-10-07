namespace GameBase.Indicators
{
    public class IndicatorFactory
    {
        public static Indicator Get(IndicatorType type)
        {
            if (type == IndicatorType.Circle)
            {
                var e = IndicatorSys.Instance.NewEntity((Indicator e) =>
                {
                    e.ObjID = 45;
                    e.textureID = 10;
                });
                e.Obj.SetActive(false);
                return e;
            }

            return null;
        }
    }
}
