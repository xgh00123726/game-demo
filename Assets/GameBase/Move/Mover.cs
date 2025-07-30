using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class Mover
    {
        public GameObject body;
        public float speed;

        internal bool targetCommand;
        internal bool destCommand;
        internal PossibleObj<Rigidbody> rigidbody;
        internal Vector3 dest;
        internal PossibleObj<GameObject> target;

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
                targetCommand = true;
                target.Set(value);
            }
        }
    }
}
