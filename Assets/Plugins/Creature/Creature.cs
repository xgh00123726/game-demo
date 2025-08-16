using GameBase.Tools;
using System;
using UnityEngine;

namespace GameBase.Creatures
{
    public class Creature : IUEntity<GameObject>
    {
        public int radius;
        public Vector3 genPos;
        public bool Alive { get; internal protected set; }
        public int InstanceID { get; set; }
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        public virtual bool ReleaseTrigger { get; } = false;

        public virtual Action AfterInstantiateObj { get; set; } = null;
    }
}
