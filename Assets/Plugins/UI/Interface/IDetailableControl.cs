namespace GameBase.UI
{
    public interface IDetailableControl<T>
    {
        public bool IsDetail(T e);
        public void OnDetail(T e);
        public void OnEnterDetail(T e);
        public void OnExitDetail(T e);
    }
}
