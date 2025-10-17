using UnityEngine;

namespace GameBase.Indicators
{
    public class RectIndicator : CastIndicator
    {
        public Indicator indicator;

        public Vector3 Dir
        {
            set
            {
                float x = value.x;
                float z = value.z;
                float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
                indicator.obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            }
            get
            {
                return indicator.obj.transform.right;
            }
        }

        public override void Hide()
        {
            indicator.obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            indicator.obj.transform.position = config.position;
        }

        public override void Show()
        {
            indicator.obj.SetActive(true);
        }
    }
}
