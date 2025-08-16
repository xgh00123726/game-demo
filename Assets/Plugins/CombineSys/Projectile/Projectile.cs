using GameBase.Flyings;
using GameBase.GEffects;
using GameBase.Math;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combines.Projectiles
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
        public Func<GEffect<IProjectileOwner, IProjectileTarget>> effectConstructor;

        // optional
        public bool hasWhite;

        internal bool flyingGeneratedObj;
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
            flyingGeneratedObj = false;
        }

        void IPoolable.BeforeRelease()
        {
            whites = null;
            flying = null;
            shape = null;
            targetsSet = null;
            target = null;
            owner = null;
            effectConstructor = null;
        }
    }
}
