using System;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Flyings
{
    public class Flying : ICurveable,
        IUEntity<GameObject>,
        IPoolable
    {
        public float releaseDistance;
        public int releaseEffectID;
        public int trailID;
        public float maxExistTime;
        
        public Vector3 dest;
        public Vector3 src;
        public Vector3 srcOffset;
        public CurveBase curve;
        public float speed;
        public CurveFactory.CurveType curveType;

        internal bool alive;
        internal GameObject releaseEffect;
        internal GameObject trail;
        internal float instantiateTime;

        public virtual void AfterGet()
        {
            releaseDistance = 0.1f;
            alive = true;
        }

        public virtual void BeforeRelease()
        {
            srcOffset = Vector3.zero;
            src = Vector3.zero;
            speed = 0;
            curveType = CurveFactory.CurveType.None;
            curve = null;
            alive = false;
            ObjID = -1;
        }

        public bool Alive => alive;
        public int InstanceID { get; set; }
        public Vector3 Dest => dest;
        public Vector3 Src => src + srcOffset;
        Vector3 ICurveable.Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }
        Vector3 ICurveable.Dir
        {
            get => Obj.transform.forward;
            set => Obj.transform.forward = value;
        }
        Vector3 ICurveable.Dest => Dest;

        Vector3 ICurveable.Src => Src;

        float ICurveable.LifeTime => Time.time - instantiateTime;

        public GameObject Obj { get; set; }
        public int ObjID { get; set; }

        public virtual Action AfterInstantiateObj { get; set; } = null;
    }
}
