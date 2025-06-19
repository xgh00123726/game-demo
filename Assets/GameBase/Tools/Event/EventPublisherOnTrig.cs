namespace GameBase.Tools
{
    public class EventPublisherOnTrig<T> : EventPublisher<T>
    {
        public delegate bool PublisherTriggerAction();
        public EventPublisherOnTrig(T data, PublisherTriggerAction trigger)
        {
            _data = data;
            _trigger = trigger;

            Eventer.AddUpdate(Update);
        }

        private void Update()
        {
            if (_trigger?.Invoke() == true)
            {
                ForcePublish(_data);
            }
        }

        private T _data;
        private PublisherTriggerAction _trigger;
    }
}
