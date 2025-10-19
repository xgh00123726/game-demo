using GameBase.Math;
using UnityEngine;

namespace GameBase.Move
{
    public class FCRectCollider : RectCollider
    {
        public float filletedCornerR = 0.1f;
        public float fcrFactor = 0.2f;

        public GMath.Circle LeftBottomCircle
        {
            get => new GMath.Circle()
            {
                r = filletedCornerR,
                c = new Vector2(xMin + filletedCornerR, yMin + filletedCornerR)
            };
        }
        public GMath.Circle LeftTopCircle
        {
            get => new GMath.Circle()
            {
                r = filletedCornerR,
                c = new Vector2(xMin + filletedCornerR, yMax - filletedCornerR)
            };
        }
        public GMath.Circle RightBottomCircle
        {
            get => new GMath.Circle()
            {
                r = filletedCornerR,
                c = new Vector2(xMax - filletedCornerR, yMin + filletedCornerR)
            };
        }
        public GMath.Circle RightTopCircle
        {
            get => new GMath.Circle()
            {
                r = filletedCornerR,
                c = new Vector2(xMax - filletedCornerR, yMax - filletedCornerR)
            };
        }


        protected internal override void Update()
        {
            base.Update();
            filletedCornerR = Mathf.Min(w, h) * fcrFactor;
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
