using UnityEngine;

namespace GameBase.Move
{
    public class Rotater
    {
        public float turnSpeed;
        public GameObject body;
        
        internal Vector3 dirSet;
        internal Vector3 dir;
        public Vector3 Dir
        {
            get => dir;
            set => dirSet = value;
        }
    }
}
