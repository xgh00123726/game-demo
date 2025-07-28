using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public class Projectile : ICurveProjectile,
        IPoolableObject,
        IEntity<GameObject>
    {
        public int bodyID;
        public int hitEffectID;
        public int trailID;
        public float maxExistTime;
        public float damage;
        public Vector3 dest;
        public Vector3 src;
        public PossibleObj<IProjectileTarget> target;
        public PossibleObj<IProjectileOwner> owner;
        public bool isImmediately;
        public float speed;
        public int penetrate;
        public bool hitWhiteListEnable;

        public CurveFactory.CurveType curveType;

        internal CurveBase curve;
        internal GameObject body;
        internal GameObject hitEffect;
        internal GameObject trail;
        internal LinkedList<IProjectileTarget> whites;


        internal float instantiateTime;

        public Vector3 Dest => target.Exist ? target.Get().Center : dest;
        public Vector3 Src => owner.Exist ? owner.Get().HandPostion : src;
        public float DisToTarget { get; internal set; }
        Vector3 ICurveProjectile.Position
        {
            get => body.transform.position;
            set => body.transform.position = value;
        }
        Vector3 ICurveProjectile.Dir
        {
            get => body.transform.forward;
            set => body.transform.forward = value;
        }
        Vector3 ICurveProjectile.Dest => Dest;

        Vector3 ICurveProjectile.Src => Src;

        float ICurveProjectile.LifeTime => Time.time - instantiateTime;

        GameObject IEntity<GameObject>.Obj
        {
            get => body;
            set => body = value;
        }
        int IEntity<GameObject>.ID => bodyID;

        void IPoolableObject.OnInstantiate()
        {
            bodyID = -1;
            hitEffectID = -1;
            trailID = -1;
            maxExistTime = 10f;
            damage = 99;
            speed = 10f;
            penetrate = 1;
            hitWhiteListEnable = false;
        }

        void IPoolableObject.OnRelease()
        {
            
        }
    }
}
