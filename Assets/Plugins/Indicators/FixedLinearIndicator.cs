using UnityEngine;

namespace GameBase.Indicators
{
    public class FixedLinearIndicator : CastIndicator
    {
        public Indicator Indicator { get; set; }

        private void SetLength(float value)
        {
            Indicator.Size = new Vector3(0.2f, value);
            Indicator.Pivot = new Vector3(0f, value / 2, 0f);
        }

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
            Indicator.Obj.transform.position = config.position;
            Vector3 dir = config.targetPosition - config.position;
            float x = dir.x;
            float z = dir.z;
            float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
            Indicator.Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            SetLength(config.length);
        }
    }
}
