using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creature
{
    public class Creature : IUEntity<GameObject>
    {
        public int radius;
        public Vector3 genPos;

        public int InstanceID { get; set; }

        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
    }
}
