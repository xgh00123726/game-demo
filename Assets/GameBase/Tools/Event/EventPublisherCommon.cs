namespace GameBase.Tools
{
    public class EventPublisherCommon<T> : EventPublisher<T>
    {
        public void Publish(T data)
        {
            ForcePublish(data);
        }
    }
}
