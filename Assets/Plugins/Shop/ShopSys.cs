using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Resources;
using UnityEngine;
namespace GameBase.Shops
{
    public class ShopSys : UObjEntitySys<Shop, GameObject, ShopSys>
    {
        protected override GameObject InstantiateObj(Shop e)
        {
            return GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));
        }

        protected override void UpdateEntity(Shop e)
        {
            if (GMath.GameDistance(e.Obj.transform.position, e.shoper.Position) < e.detectRange)
            {
                e.newrView?.Show(e.Obj.transform.position);

                if (!e.isOpen && e.shoper.OpenShop)
                {
                    e.shopView.Show(); 
                    e.shopView.SetItem(e.model.currGoodIndexInModel);
                    e.isOpen = true;
                }
                else if (e.isOpen && e.shoper.CloseShop)
                {
                    e.shopView.Hide();
                    e.isOpen = false;
                }
            }
            else
            {
                e.newrView?.Hide();
                e.shopView.Hide();
            }

            if (e.isOpen)
            {
                if (e.interactive.TrigRefresh)
                {
                    e.model.Refresh();
                    e.shopView.SetItem(e.model.currGoodIndexInModel);
                    e.interactive.OnTrigRefresh();
                }

                var currPurchase = e.interactive.CurrentPurchase;
                if (currPurchase >= 0)
                {
                    var isPurchaseOK = e.model.Purchase(currPurchase, e.shoper);
                    if (isPurchaseOK)
                    {
                        e.shopView.SetItem(e.model.currGoodIndexInModel);
                    }
                }
            }

            //if (e.isOpen && !e.lastOpen)
            //{
            //    e.shopView.SetItem(e.model.currGoodIndexInModel);
            //}

            e.lastOpen = e.isOpen;
        }
    }
}
