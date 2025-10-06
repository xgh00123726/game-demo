using UnityEngine;

namespace GameBase.Indicators
{
    public class LinearIndicator : CastIndicator
    {
        public Indicator indicator;
        public float Length
        {
            set
            {
                SetLength(value);
            }
        }

        private void SetLength(float value)
        {
            indicator.Size = new Vector3(0.2f, value);
            indicator.Pivot = new Vector3(0f, value / 2, 0f);
        }

        public override void Show()
        {
            indicator.Obj.SetActive(true);
        }

        public override void Hide()
        {
            indicator.Obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            indicator.Obj.transform.position = config.position;
            Vector3 dir = config.targetPosition - config.position;
            float x = dir.x;
            float z = dir.z;
            float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
            indicator.Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            SetLength(dir.magnitude);
        }
    }
}
