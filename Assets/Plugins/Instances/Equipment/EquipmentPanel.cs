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
                    this[i].triggerImage.SetIcon(equipment.textureName);
                    this[i].triggerImage.Show();
                    this[i].triggerImage.SetColor(equipment.rarity);
                }
                else
                {
                    this[i].triggerImage.Hide();
                    this[i].triggerImage.HideColor();
                }
            }
        }
    }
}
