using System;
using GameBase.EntitySystem;
using GameBase.Triggers;
using UnityEngine;

namespace GameBase.Flyings
{
    public class Flying : ICurveable,
        IKeyEntity<string>,
        ITriggerAttach,
        IFlyingTarget
    {
        public float ArriveDis { get; set; }
        public float MaxExistTime { get; set; }
        public float MinExistTime { get; set; }
        public IFlyingTarget Target { get; set; }
        public float StartAngleOffset { get; set; }
        public Vector3 Dest {  get; set; }

        public string ReleaseEffect { get; set; }
        public string ReleaseAudio { get; set; }
        public Action OnReleased { get; set; }

        internal float speed;
        internal Vector3 src;
        internal bool alive;
        internal float instantiateTime;
        internal Curve curve;

        public Curve Curve
        {
            get => curve;
            set
            {
                curve = value;
                curve?.SetOwner(this);
            }
        }
        public float Speed
        {
            get => speed;
            set
            {
                speed = value;
                if (Curve != null)
                {
                    Curve.Speed = value;
                }
            }
        }
        public bool IsEnd => Curve?.IsEnd == true;

        public bool Alive => alive;
        public Vector3 Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }
        public Vector3 Dir
        {
            get => Obj.transform.forward;
            set => Obj.transform.forward = value;
        }

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
        public string Key { get; set; }

        Vector3 ITriggerAttach.Position => Obj.transform.position;
    }
}
