using GameBase.Buffs;
using GameBase.UI;
using System;

namespace Instance.Buffs
{
    public class ViewableEquipment : Buff,
        IViewableEquipment
    {
        public int textureID;
        private static DetailUI detailUI;

        public float DurationRemain => durationRemain;

        public float DurationSet => durationSet;

        public int StackNum => stackNum;

        public int TextureID => textureID;

        int IViewableEquipment.Position => 0;

        static ViewableEquipment()
        {
            detailUI = DetailUISys.Instance.NewEntity<DetailUI>();
        }

        public ViewableEquipment()
        {
            RegistertoActivesDelegate.Add(ShowBuffUI);
        }

        private void ShowBuffUI(Buff b)
        {
            var equipmentUI = EquipmentPanel.Instance.NewEntity<EquipmentItem>();
            equipmentUI.bindEquipment = this;

            equipmentUI.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
            {
                detailUI.detailables.Add(equipmentUI);
            };
        }
    }
}
