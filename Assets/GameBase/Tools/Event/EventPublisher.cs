using System.Collections.Generic;
namespace GameBase.Tools
{
    public abstract class EventPublisher<T>
    {
        public EventPublisher()
        {
            _subscribers = new List<EventSubscriber<T>> ();
        }

        protected void ForcePublish(T data)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnPublish?.Invoke(data);
            }
        }
        public EventSubscriber<T> CreateSubscriber()
        {
            var subscriber = new EventSubscriber<T>();
            subscriber.Subscribe(this);
            return subscriber;
        }

        internal List<EventSubscriber<T>> _subscribers;
    }
}
