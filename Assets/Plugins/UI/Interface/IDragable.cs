namespace GameBase.UI
{
    public interface IDragable<T>
    {
        bool IsDrag(T e);
        void OnEnterDrag(T e);
        void OnExitDrag(T e);
        void OnDrag(T e);
    }
}
