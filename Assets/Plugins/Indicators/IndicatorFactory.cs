namespace GameBase.Indicators
{
    public class IndicatorFactory
    {
        public static Indicator Get(IndicatorType type)
        {
            if (type == IndicatorType.Circle)
            {
                var e = IndicatorSys.Instance.NewEntity("Prefabs/Info/CreatureCircleIndicator.prefab");
                e.TextureName = "Textures/Circle";
                e.Obj.SetActive(false);
                return e;
            }

            return null;
        }
    }
}
