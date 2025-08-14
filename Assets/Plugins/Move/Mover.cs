using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class Mover : IEntity
    {
        public IMover owner;
        public bool IsMoving => isMoveing;

        internal bool isMoveing;
        internal bool targetCommand;
        internal bool destCommand;
        internal PossibleObj<Rigidbody> rigidbody;
        internal Vector3 dest;
        internal PossibleObj<GameObject> target;

        public int InstanceID { get; set; }

        public Rigidbody RigidyBody
        {
            get => rigidbody.Get();
            set => rigidbody.Set(value);
        }

        public Vector3 Dest
        {
            get => target.Exist ? target.Get().transform.position : dest;
            set
            {
                destCommand = true;
                isMoveing = true;
                targetCommand = false;
                dest = value;
            }
        }
        public GameObject Target
        {
            get => target.Get();
            set
            {
                destCommand = false;
                isMoveing = true;
                targetCommand = true;
                target.Set(value);
            }
        }
    }
}
