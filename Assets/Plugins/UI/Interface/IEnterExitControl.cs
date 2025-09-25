namespace GameBase.UI
{
    public interface IEnterExitControl
    {
        void OnPointerEnter(int i);
        void OnPointerExit(int i);
        void OnPointerDown(int i);
        void OnPointerRightDown(int i);
    }
}
