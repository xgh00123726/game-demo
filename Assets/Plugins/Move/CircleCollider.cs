using GameBase.Math;
using UnityEngine;

namespace GameBase.Move
{
    public class CircleCollider : Collider
    {
        public float r;
        public override void CollideTo(Collider other)
        {
            if (other is CircleCollider cc)
            {
                CollideUtil.CircleCollideToCircle(this, cc);
            }
            else if (other is FCRectCollider fcrc)
            {
                CollideUtil.CircleCollideToFCRect(this, fcrc);
            }
            else if (other is RectCollider rc)
            {
                CollideUtil.CircleCollideToRect(this, rc);
            }
        }
    }
}
