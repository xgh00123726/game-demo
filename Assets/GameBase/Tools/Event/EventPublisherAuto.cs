using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.Tools
{
    public class EventPublisherAuto<T> : EventPublisher<T>
    {
        public EventPublisherAuto(T dataRef)
        {
            Assert.IsTrue(typeof(T).IsClass);

            _data = dataRef;
            _lastData = dataRef;

            Eventer.AddUpdate(Update);
        }

        private void Update()
        {
            if (_data.Equals(_lastData)) return;
            ForcePublish(_data);

            _lastData = _data;
        }

        private T _data;
        private T _lastData;
    }
}
