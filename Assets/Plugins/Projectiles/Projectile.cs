using System;
using System.Collections.Generic;
using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public class Projectile : ICurveProjectile,
        IUEntity<GameObject>
    {
        public Projectile()
        {
            XLogger.Instance.Log("proj constructror");
        }

        public int hitEffectID;
        public int trailID;
        public float maxExistTime;
        public float damage;
        public Vector3 dest;
        public Vector3 src;
        public CurveBase curve;
        public PossibleObj<IProjectileTarget> target;
        public PossibleObj<IProjectileOwner> owner;
        public IShape2D shape;
        public bool isImmediately;
        public float speed;
        public int penetrate;
        public bool whiteEnable;
        public int tickRate;
        public int hitInterval;
        public Action<Projectile> OnAlive;
        public Action<Projectile> OnAliveFixed;

        public CurveFactory.CurveType curveType;

        internal int actualPenetrate;
        internal int tick;
        internal GameObject hitEffect;
        internal GameObject trail;
        public PossibleObj<HashSet<int>> whites;


        internal float instantiateTime;

        public int Tick => tick;

        public float Size
        {
            set
            {
                if (shape != null)
                {
                    shape.Size = value;
                    Obj.transform.localScale = new Vector3(value, value, value);
                }
            }
        }

        public int InstanceID { get; set; }
        public Vector3 Dest => target.Exist ? target.Get().Center : dest;
        public Vector3 Src => src;
        public float DisToTarget { get; internal set; }
        Vector3 ICurveProjectile.Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }
        Vector3 ICurveProjectile.Dir
        {
            get => Obj.transform.forward;
            set => Obj.transform.forward = value;
        }
        Vector3 ICurveProjectile.Dest => Dest;

        Vector3 ICurveProjectile.Src => Src;

        float ICurveProjectile.LifeTime => Time.time - instantiateTime;

        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
    }
}
