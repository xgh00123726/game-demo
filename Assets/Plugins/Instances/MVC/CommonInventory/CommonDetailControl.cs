using GameBase.UI;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class CommonDetailControl : IDetailableControl<CommonInventoryViewItem>
    {
        private CommonInventoryController _CC;
        private DefaultDetailableShadowView _shadowView = new();
        protected DefaultDetailableShadowView ShadowView => _shadowView;

        public CommonDetailControl(CommonInventoryController CC)
        {
            _CC = CC;
        }

        bool IDetailableControl<CommonInventoryViewItem>.IsDetail(CommonInventoryViewItem e)
        {
            return e.Obj.EnterTime > 0.2f && _CC.HasItem(e.ItemIndex);
        }

        void IDetailableControl<CommonInventoryViewItem>.OnDetail(CommonInventoryViewItem e)
        {
            ShadowView.Show();
        }

        void IDetailableControl<CommonInventoryViewItem>.OnEnterDetail(CommonInventoryViewItem e)
        {
            ShadowView.SetPosition(Input.mousePosition);

            if (_CC.HasItem(e.ItemIndex))
            {
                ShadowView.SetText(InventoryDataBase.GetText(_CC[e.ItemIndex].dItem.ID));
            }
            else
            {
                ShadowView.SetText($"index:{e.ItemIndex}");
            }
        }

        void IDetailableControl<CommonInventoryViewItem>.OnExitDetail(CommonInventoryViewItem e)
        {
            ShadowView.Hide();
        }
    }
}
