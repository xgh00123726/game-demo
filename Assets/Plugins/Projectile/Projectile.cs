using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.GEffects;
using GameBase.Math;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Projectiles
{ 
    public class Projectile : IEntity,
        IPoolable
    {
        // require
        public Flying flying;
        public int maxeffectTimes;
        public IProjectileTarget target;
        public IShape2D shape;
        public IProjectileTargetsSet targetsSet;
        public IProjectileOwner owner;
        public IProjectileAction action;

        // optional
        public bool hasWhite;

        internal int actualEffectTimes;
        internal HashSet<int> whites;

        public int InstanceID { get; set; }

        public float Size
        {
            set
            {
                if (shape != null)
                {
                    shape.Size = value;
                    flying.Obj.transform.localScale = new Vector3(value, value, value);
                }
            }
        }

        void IPoolable.AfterGet()
        {
            actualEffectTimes = 0;
            maxeffectTimes = 1;
            hasWhite = false;
        }

        void IPoolable.BeforeRelease()
        {
            whites = null;
            flying = null;
            shape = null;
            targetsSet = null;
            target = null;
            owner = null;
            action = null;
        }
    }
}
