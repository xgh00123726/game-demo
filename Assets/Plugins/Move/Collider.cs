using UnityEngine;

namespace GameBase.Move
{
    public class Collider
    {
        public Vector2 LocalPosition { get; set; }
        public bool IsCollide { get; internal set; }
        public Vector2 Force { get; internal set; }

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
                position = new Vector2(LocalPosition.x + owner.position.x, LocalPosition.y + owner.position.z);
            }
            else
            {
                position = LocalPosition;
            }
        }
        public virtual void CollideTo(Collider other) { }
    }
}
