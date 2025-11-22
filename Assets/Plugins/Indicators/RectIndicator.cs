using UnityEngine;

namespace GameBase.Indicators
{
    public class RectIndicator : CastIndicator
    {
        public Indicator Indicator { get; set; }

        public Vector3 Dir
        {
            set
            {
                float x = value.x;
                float z = value.z;
                float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
                Indicator.Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            }
            get
            {
                return Indicator.Obj.transform.right;
            }
        }

        public override void Hide()
        {
            Indicator.Obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            Indicator.Obj.transform.position = config.position;
        }

        public override void Show()
        {
            Indicator.Obj.SetActive(true);
        }
    }
}
