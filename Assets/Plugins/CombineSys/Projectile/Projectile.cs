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
        public int maxeffectTimes;
        public bool hasWhite;

        public IShape2D shape;
        public IProjectileTargetsSet targetsSet;
        public Func<GEffect<IProjectileOwner, IProjectileTarget>> effectConstructor;
        public IProjectileTarget target;
        public IProjectileOwner owner;

        internal Flying flying;
        internal int actualEffectTimes;
        internal HashSet<int> whites;
        internal bool updateEnable;

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
            flying = FlyingSys.Instance.NewEntity<Flying>(4);
            flying.AfterInstantiateObj = () => updateEnable = true;
        }

        void IPoolable.BeforeRelease()
        {
            maxeffectTimes = 1;
            hasWhite = false;
            whites = null;
            flying = null;
            shape = null;
            targetsSet = null;
            target = null;
            owner = null;
            effectConstructor = null;
            updateEnable = false;
        }
    }
}
