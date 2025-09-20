using GameBase.Spells;
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
                indicator.Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            }
            get
            {
                return indicator.Obj.transform.right;
            }
        }

        public override void Hide()
        {
            indicator.Obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            indicator.Obj.transform.position = config.position;
        }

        public override void Show()
        {
            indicator.Obj.SetActive(true);
        }
    }
}
