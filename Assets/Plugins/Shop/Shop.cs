using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Shops
{
    public class Shop : IUEntity<GameObject>
    {
        internal bool isOpen = false;

        public float detectRange = 2f;

        public IShoper shoper;
        public INearView newrView;
        public IShopView shopView;
        public IShopInteractive interactive;
        public ShopModel model;

        public int GoodNum
        {
            get => model.GoodNums;
            set
            {
                model.GoodNums = value;
                shopView.GoodNums = value;
            }
        }

        public GameObject Obj { get; set; }
        public int ObjID { get; set; } = 1;
    }
}
