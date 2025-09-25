namespace GameBase.UI
{
    public interface IDetailableControl
    {
        public bool IsDetail(int i);
        public void OnDetail(int i);
        public void OnEnterDetail(int i);
        public void OnExitDetail(int i);
    }
}
