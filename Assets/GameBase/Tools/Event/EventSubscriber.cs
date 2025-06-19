using System.Collections;
using System.Collections.Generic;
namespace GameBase.Tools
{
    public class EventSubscriber<T>
    {
        public delegate void OnPublishEventHandle(T eventData);

        public OnPublishEventHandle OnPublish;

        public void Subscribe(EventPublisher<T> publisher)
        {
            publisher._subscribers.Add(this);
        }
    }
}
