using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Shops
{
    public interface IShopView
    {
        void SetItem(List<int> goodIDs);
        void Show();
        void Hide();
    }
}
