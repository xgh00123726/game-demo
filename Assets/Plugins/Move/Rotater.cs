using GameBase.Tools;
using UnityEngine;

namespace GameBase.Move
{
    public class Rotater : IEntity
    {
        public float turnSpeed;
        public GameObject body;

        internal int id;
        internal Vector3 dirSet;
        internal Vector3 dir;

        public int ID
        {
            get => id;
            set => id = value;
        }
        public Vector3 Dir
        {
            get => dir;
            set => dirSet = value;
        }
    }
}
