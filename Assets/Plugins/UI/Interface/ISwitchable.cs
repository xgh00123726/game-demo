namespace GameBase.UI
{
    public interface ISwitchable<T>
    {
        void OnSwitchOn(T e);
        void OnSwitchOff(T e);
    }
}
