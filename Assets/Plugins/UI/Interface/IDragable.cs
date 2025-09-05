namespace GameBase.UI
{
    /// <summary>
    /// 当panel中有item被拖动时触发，入参e为panel判定为拖动时鼠标位置所在的item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDragable<T>
    {
        bool IsDrag(T e);
        void OnEnterDrag(T e);
        void OnExitDrag(T e);
        void OnDrag(T e);
    }
}
