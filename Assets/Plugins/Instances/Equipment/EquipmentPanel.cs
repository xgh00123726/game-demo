using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Tools;
using GameBase.UI;

namespace Instance
{
    public class EquipmentPanel : BaseViewPanel<EquipmentItem, EquipmentPanel>
    {
        protected override string PanelPrefabName => "Prefabs/UI/EquipmentPanel";
        protected override string ItemPrefabName => "Prefabs/UI/EquipmentItem";

        public void UpdatePanel(Creature c)
        {
            for (int i = 0; i < Entities.Count; ++i)
            {
                var equipment = c.GetEquipment(i);
                if (equipment != null)
                {
                    this[i].TriggerImage.SetIcon(equipment.TextureName);
                    this[i].TriggerImage.Show();
                    this[i].TriggerImage.SetColor(equipment.Rarity);
                }
                else
                {
                    this[i].TriggerImage.Hide();
                    this[i].TriggerImage.HideColor();
                }
            }
        }
    }
}
