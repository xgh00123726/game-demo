using GameBase.UI;

namespace Instance.UI.MVC
{
    public class MVCDetailableControl<T> : IDetailableControl<T>
        where T : BaseViewItem
    {
        protected virtual void OnDetail(T e) { }

        bool IDetailableControl<T>.IsDetail(T e)
        {
            return e.Obj.EnterTime > 0.2f;
        }

        void IDetailableControl<T>.OnDetail(T e)
        {
            OnDetail(e);
        }

        void IDetailableControl<T>.OnEnterDetail(T e)
        {
        }

        void IDetailableControl<T>.OnExitDetail(T e)
        {
        }
    }
}
