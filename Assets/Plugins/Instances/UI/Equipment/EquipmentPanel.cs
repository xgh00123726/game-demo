using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Tools;
using GameBase.UI;

namespace Instance
{
    public class EquipmentPanel : BaseViewPanel<EquipmentItem>
    {
        public const int EQUIPMENT_NUM = 6;
        private static EquipmentPanel _instance;
        public static EquipmentPanel Instance => _instance;
        public EquipmentPanel(int prefabID = 17, int defaultObjID = 18) : base(prefabID, defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _instance = this;
            FillItem(EQUIPMENT_NUM);
            Show();
        }

        public void UpdateView(Creature c)
        {
            for (int i = 0; i < EQUIPMENT_NUM; ++i)
            {
                if (c.HasEquipment(i))
                {
                    Buff equipment = c.GetEquipment(i);
                    var info = BuffDataBase.Instance[equipment.id];
                    this[i].triggerImage.SetIcon(info.iconTextureID);
                    this[i].triggerImage.SetColor(info.rarity);
                }
                else
                {
                    this[i].triggerImage.Hide();
                }
            }
        }
    }
}
