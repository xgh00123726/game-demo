using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.Move
{
    public class Mover
    {
        internal bool isMoving;
        internal Vector3 dest;
        internal bool isArrive;

        public IMover owner;

        public bool IsMoving => isMoving;
        public Vector3 Dest => dest;

        public bool IsArrive => isArrive;
        public IMover Owner
        {
            get => owner;
            set
            {
                owner = value;
                dest = owner.Position;
            }
        }

        public void MoveTo(Vector3 position)
        {
            isMoving = true;
            dest = position;
        }
    }
}
