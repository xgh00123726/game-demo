using UnityEngine;

namespace GameBase.Tools.Transforms
{
    internal class MoveTask : ITask
    {
        private float _beginTime;
        private Vector3 _beginPosition;
        private Transform _obj;
        private Vector3 _targetPosition;
        private float _duration;
        
        public bool IsOver { get; private set; }
        public TaskResult Result { get; set; }

        internal MoveTask(Transform obj, Vector3 targetPosition, float duration)
        {
            _beginTime = Time.time;
            _beginPosition = obj.position;
            _obj = obj;
            _targetPosition = targetPosition;
            _duration = duration;
            IsOver = false;
        }

        void ITask.Update()
        {
            if (_duration <= 0)
            {
                IsOver = true;
                _obj.position = _targetPosition;
                return;
            }
            if (Time.time - _beginTime >= _duration)
            {
                IsOver = true;
                _obj.position = _targetPosition;
            }

            var usedTime = Time.time - _beginTime;
            _obj.position = Vector3.Lerp(_beginPosition, _targetPosition, usedTime / _duration);
        }
    }
}
