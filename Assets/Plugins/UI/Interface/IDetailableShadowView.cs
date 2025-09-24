using UnityEngine;

namespace GameBase.UI
{
    public interface IDetailableShadowView
    {
        void SetPosition(Vector3 position);
        void SetText(string text);
        void Show();
        void Hide();
    }
}
