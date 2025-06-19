using System;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.Object
{
    public partial class CruiseComponent : GComponent
    {
        public CuriseBase _curise;
        public CuriseBase Curise { set => _curise = value; }
        public float _trackBeginTime = 0f;
        public float _maxTrackTime = 10f;
        public float _maxTrackDis = 10f;
        public float _arriveDis = 0.1f;
        public bool _inTrack = false;

        public Vector3 _lastPosition;

        private Action _OnTrackEnd;
        public Action OnTrackEnd
        {
            set => _OnTrackEnd = value;
        }
        public bool InTrack => _inTrack;

        public void StartTrack(Vector3 end)
        {
            _inTrack = true;
            _trackBeginTime = Time.time;
            _curise.start = transform.position;
            _curise.end = end;
            _curise.ReInit();
        }

        protected void Awake()
        {
            _curise = new CuriseDefault();
        }
        protected void Update()
        {
            if (!_inTrack) return;
            if (_curise == null) return;
            float timeRelative = Time.time - _trackBeginTime;
            transform.position = _curise.TrackEquation(timeRelative);
            
            if ((_curise.end - transform.position).magnitude <= _arriveDis
                || Time.time - _trackBeginTime > _maxTrackTime
                || (transform.position - _curise.start).magnitude > _maxTrackDis)
            {
                _inTrack = false;
                _OnTrackEnd?.Invoke();
            }

            Vector3 delta = transform.position - _lastPosition;
            transform.forward = delta;
            _lastPosition = transform.position;
        }
    }
}
