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
        public float arriveDis;
        public float maxExistTime;
        public float minExistTime;
        
        public IFlyingTarget target;
        public float startAngleOffset;
        public Vector3 dest;
        public Curve curve;

        public string releaseEffect;
        public string releaseAudio;

        public Action OnReleased;

        internal float speed;
        internal Vector3 src;
        internal bool alive;
        internal float instantiateTime;
        public float Speed
        {
            get => speed;
            set
            {
                speed = value;
                if (curve != null)
                {
                    curve.speed = value;
                }
            }
        }
        public bool IsEnd => curve?.IsEnd == true;

        public bool Alive => alive;
        public Vector3 Position
        {
            get => obj.transform.position;
            set => obj.transform.position = value;
        }
        public Vector3 Dir
        {
            get => obj.transform.forward;
            set => obj.transform.forward = value;
        }
        public Vector3 Dest => dest;

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
        public string Key { get; set; }

        Vector3 ITriggerAttach.Position => obj.transform.position;
    }
}
