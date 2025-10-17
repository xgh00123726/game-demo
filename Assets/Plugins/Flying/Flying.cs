using System;
using GameBase.EntitySystem;
using GameBase.Triggers;
using UnityEngine;

namespace GameBase.Flyings
{
    public class Flying : ICurveable,
        IKeyEntity<int>,
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

        public bool Alive => alive;
        Vector3 ICurveable.Position
        {
            get => obj.transform.position;
            set => obj.transform.position = value;
        }
        Vector3 ICurveable.Dir
        {
            get => obj.transform.forward;
            set => obj.transform.forward = value;
        }
        Vector3 ICurveable.Dest => target.Position;

        public Vector3 Src
        {
            get => src;
            set
            {
                src = value;
                obj.transform.position = src;
            }
        }

        float ICurveable.LifeTime => Time.time - instantiateTime;

        public GameObject obj;
        public int Key { get; set; }

        Vector3 ITriggerAttach.Position => obj.transform.position;
    }
}
