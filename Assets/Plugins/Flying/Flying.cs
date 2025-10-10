using System;
using GameBase.EntitySystem;
using GameBase.Triggers;
using UnityEngine;

namespace GameBase.Flyings
{
    public class Flying : ICurveable,
        IUEntity<GameObject>,
        IPoolable,
        ITriggerAttach
    {
        public float arriveDis;
        public int trailID;
        public float maxExistTime;
        public float minExistTime;

        public CurveBase curve;
        public float speed;
        public CurveFactory.CurveType curveType;
        public IFlyingTarget target;

        public Action OnHit;
        public Action OnReleased;

        internal bool hitFlag;
        internal Vector3 src;
        internal bool alive;
        internal GameObject trail;
        internal float instantiateTime;

        public virtual void AfterGet()
        {
            arriveDis = 0.1f;
            maxExistTime = 10f;
            minExistTime = 0f;
            speed = 5f;
            alive = true;
            hitFlag = false;
        }

        public virtual void BeforeRelease()
        {
            speed = 0;
            curveType = CurveFactory.CurveType.None;
            curve = null;
            alive = false;
            OnReleased = null;
            OnHit = null;
            ObjID = -1;
        }

        public bool Alive => alive;
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
        Vector3 ICurveable.Dest => target.Position;

        public Vector3 Src
        {
            get => src;
            set
            {
                src = value;
                Obj.transform.position = src;
            }
        }

        float ICurveable.LifeTime => Time.time - instantiateTime;

        public GameObject Obj { get; set; }
        public int ObjID { get; set; }

        Vector3 ITriggerAttach.Position => Obj.transform.position;
    }
}
