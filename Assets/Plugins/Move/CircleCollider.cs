using UnityEngine;

namespace GameBase.Move
{
    public class CircleCollider : Collider
    {
        public float r;

        private void CollideToCircleCollider(CircleCollider other)
        {
            var c1 = this;
            var c2 = other;
            var x1 = c1.position.x;
            var y1 = c1.position.y;
            var x2 = c2.position.x;
            var y2 = c2.position.y;
            var r1 = c1.r;
            var r2 = c2.r;

            float dx = x2 - x1;
            float dy = y2 - y1;
            var force = new Vector2(dx, dy);
            var forceLen = force.magnitude;
            float intersectLen = r1 + r2 - forceLen;

            if (intersectLen > 0)
            {
                c1.isCollide = true;
                c2.isCollide = true;

                c1.force += force.normalized * -intersectLen;
                c2.force += force.normalized * intersectLen;
            }
        }

        public override void CollideTo(Collider other)
        {
            if (other is CircleCollider cc)
            {
                CollideToCircleCollider(cc);
            }
        }
    }
}
