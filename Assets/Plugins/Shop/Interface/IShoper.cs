using UnityEngine;

namespace GameBase.Shops
{
    public interface IShoper
    {
        int Gold { get; set; }
        Vector3 Position { get; }
        bool OpenShop { get; }
        bool CloseShop { get; }
        void OnPurchaseItem(int goodID);
    }
}
