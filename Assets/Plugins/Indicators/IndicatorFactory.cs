namespace GameBase.Indicators
{
    public class IndicatorFactory
    {
        public static Indicator Get(IndicatorType type)
        {
            if (type == IndicatorType.Circle)
            {
                var e = IndicatorSys.Instance.NewEntity("Prefabs/Info/CreatureCircleIndicator.prefab");
                e.textureName = "Textures/Circle";
                e.obj.SetActive(false);
                return e;
            }

            return null;
        }
    }
}
