using UnityEngine;

namespace GameBase.Move
{
    public class Collider
    {
        public Vector2 position;
        public bool isCollide;
        public Vector2 force;
        public Transform owner;

        public virtual void CollideTo(Collider other) { }
    }
}
