namespace GameBase.Indicators
{
    public class CircleIndicator : CastIndicator
    {
        public Indicator indicator;

        public override void Show()
        {
            indicator.obj.SetActive(true);
        }

        public override void Hide()
        {
            indicator.obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            indicator.obj.transform.position = config.targetPosition;
        }
    }
}
