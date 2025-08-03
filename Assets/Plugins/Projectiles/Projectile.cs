using System;
using System.Collections.Generic;
using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public class Projectile : ICurveProjectile,
        IPoolableObject,
        IUEntity<GameObject>
    {
        public int bodyID;
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
        internal int id;
        internal int tick;
        internal GameObject body;
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
                    body.transform.localScale = new Vector3(value, value, value);
                }
            }
        }

        public int ID
        {
            get => id;
            set => id = value;
        }
        public Vector3 Dest => target.Exist ? target.Get().Center : dest;
        public Vector3 Src => src;
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

        public GameObject Obj
        {
            get => body;
            set => body = value;
        }
        int IUEntity<GameObject>.ObjID => bodyID;

        void IPoolableObject.OnInstantiate()
        {
            // 记录射弹生成时刻
            instantiateTime = Time.time;

            if (curveType != CurveFactory.CurveType.None)
            {
                curve = CurveFactory.CreateInstance(curveType, this);
            }

            // 如果射弹是穿透性的，才给射弹设置白名单
            if (whiteEnable && penetrate > 1)
            {
                whites = PossibleObj<HashSet<int>>.New(new HashSet<int>());
            }

            if (owner.Exist)
            {
                src = owner.Get().HandPostion;
            }

            actualPenetrate = 0;
        }

        void IPoolableObject.OnRelease()
        {
            if (whites.Exist)
            {
                whites.Get().Clear();
            }
            body.transform.localScale = Vector3.one;
        }
    }
}
