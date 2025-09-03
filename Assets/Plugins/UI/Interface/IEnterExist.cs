namespace GameBase.UI
{
    public interface IEnterExist<T>
    {
        void OnPointerEnter(T e);
        void OnPointerExit(T e);
    }
}
