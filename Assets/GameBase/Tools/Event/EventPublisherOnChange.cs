namespace GameBase.Tools
{
    public class EventPublisherOnChange<T> : EventPublisher<T>
    {
        public void Publish(T data)
        {
            if (!data.Equals(_data))
            {
                ForcePublish(data);
            }
            _data = data;
        }

        private T _data;
    }
}
