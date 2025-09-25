namespace GameBase.UI
{
    /// <summary>
    /// 当panel中有item被拖动时触发，入参e为panel判定为拖动时鼠标位置所在的item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDragableControl
    {
        bool IsDrag(int i);
        void OnEnterDrag(int i);
        void OnExitDrag();
        void OnDrag();
    }
}
