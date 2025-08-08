using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class Rotater : IEntity
    {
        public float turnSpeed;
        public GameObject body;

        internal Vector3 dirSet;
        internal Vector3 dir;

        public int InstanceID { get; set; }

        public Vector3 Dir
        {
            get => dir;
            set => dirSet = value;
        }
    }
}
