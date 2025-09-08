using UnityEngine;

namespace GameBase.Shops
{
    public interface IShopView
    {
        int GoodNums { get; set; }
        void Show();
        void Hide();
    }
}
