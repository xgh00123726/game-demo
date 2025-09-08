using UnityEngine;

namespace GameBase.Shops
{
    public interface INearView
    {
        void Show(Vector3 position);
        void Hide();
    }
}
