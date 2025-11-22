using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Tools;
using GameBase.UI;

namespace Instance
{
    public class EquipmentPanel : BaseViewPanel<EquipmentItem>
    {
        private static EquipmentPanel _instance;
        public static EquipmentPanel Instance => _instance;
        public EquipmentPanel(string prefabName = "Prefabs/UI/EquipmentPanel",
            string itemPrefabName = "Prefabs/UI/EquipmentItem") : base(prefabName, itemPrefabName)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _instance = this;
        }

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
