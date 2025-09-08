namespace GameBase.UI
{
    public interface IEnterExistControl<T>
    {
        void OnPointerEnter(T e);
        void OnPointerExit(T e);
        void OnPointerDown(T e);
        void OnPointerRightDown(T e);
    }
}
