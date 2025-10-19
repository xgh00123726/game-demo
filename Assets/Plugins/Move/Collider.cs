using UnityEngine;

namespace GameBase.Move
{
    public class Collider
    {
        public Vector2 localPosition;
        public bool isCollide;
        public Vector2 force;

        internal bool hasOwner;
        internal Transform owner;
        internal Vector2 position;

        public Vector2 Position => position;
        public Transform Owner
        {
            get => owner;
            set
            {
                hasOwner = true;
                owner = value;
            }
        }
        internal protected virtual void Update()
        {
            if (hasOwner)
            {
                position = new Vector2(localPosition.x + owner.position.x, localPosition.y + owner.position.z);
            }
            else
            {
                position = localPosition;
            }
        }
        public virtual void CollideTo(Collider other) { }
    }
}
