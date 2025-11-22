using GameBase.Math;
using UnityEngine;

namespace GameBase.Move
{
    public class FCRectCollider : RectCollider
    {
        public float FilletedCornerR { get; set; } = 0.1f;
        public float FcrFactor { get; set; } = 0.2f;

        public GMath.Circle LeftBottomCircle
        {
            get => new GMath.Circle()
            {
                r = FilletedCornerR,
                c = new Vector2(xMin + FilletedCornerR, yMin + FilletedCornerR)
            };
        }
        public GMath.Circle LeftTopCircle
        {
            get => new GMath.Circle()
            {
                r = FilletedCornerR,
                c = new Vector2(xMin + FilletedCornerR, yMax - FilletedCornerR)
            };
        }
        public GMath.Circle RightBottomCircle
        {
            get => new GMath.Circle()
            {
                r = FilletedCornerR,
                c = new Vector2(xMax - FilletedCornerR, yMin + FilletedCornerR)
            };
        }
        public GMath.Circle RightTopCircle
        {
            get => new GMath.Circle()
            {
                r = FilletedCornerR,
                c = new Vector2(xMax - FilletedCornerR, yMax - FilletedCornerR)
            };
        }


        protected internal override void Update()
        {
            base.Update();
            FilletedCornerR = Mathf.Min(Width, Height) * FcrFactor;
        }

        public override void CollideTo(Collider other)
        {
            if (other is CircleCollider cc)
            {
                CollideUtil.CircleCollideToFCRect(cc, this);
            }    
        }
    }
}
