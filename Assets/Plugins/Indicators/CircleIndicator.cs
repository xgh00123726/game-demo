namespace GameBase.Indicators
{
    public class CircleIndicator : CastIndicator
    {
        public Indicator Indicator { get; set; }

        public override void Show()
        {
            Indicator.Obj.SetActive(true);
        }

        public override void Hide()
        {
            Indicator.Obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            Indicator.Obj.transform.position = config.targetPosition;
        }
    }
}
