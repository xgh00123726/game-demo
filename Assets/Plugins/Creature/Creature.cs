using GameBase.Tools;
using System;
using UnityEngine;

namespace GameBase.Creatures
{
    public enum Tag
    {
        CommonCreature = 1 << 0,
        Player = 1 << 1,
    }
    public class Creature : IUEntity<GameObject>,
        IPoolable
    {
        public int radius;
        public Vector3 genPos;
        public Tag tag;
        public bool Alive { get; internal protected set; }
        public int InstanceID { get; set; }
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        public virtual bool ReleaseTrigger { get; } = false;
        public virtual Action AfterInstantiateObj { get; set; } = null;

        public virtual void AfterGet()
        {
            tag = Tag.CommonCreature;
        }

        public void BeforeRelease()
        {
            
        }
    }
}
