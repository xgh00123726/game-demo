using UnityEngine;

namespace GameBase.Move
{
    public class RectCollider : Collider
    {
        internal float xMin;
        internal float xMax;
        internal float yMin;
        internal float yMax;

        public float Width { get; set; }
        public float Height { get; set; }
        public float XMin => xMin;
        public float XMax => xMax;
        public float YMin => yMin;
        public float YMax => yMax;

        protected internal override void Update()
        {
            base.Update();
            xMin = position.x - Width / 2;
            xMax = position.x + Width / 2;
            yMin = position.y - Height / 2;
            yMax = position.y + Height / 2;
        }

        public override void CollideTo(Collider other)
        {
            if (other is CircleCollider cc)
            {
                CollideUtil.CircleCollideToRect(cc, this);
            }
        }
    }
}
