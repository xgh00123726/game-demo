using GameBase.EntitySystem;
using GameBase.Items;

namespace GameBase.Equipments
{
    public class EquipmentFactory : MultiFactory<Equipment, EquipmentFactory>
    {
        public EquipmentFactory()
        {
            Register(EquipmentYamlFactory.Instance.Get);
        }

        public Equipment Get(int id)
        {
            var item = ItemDataMgr.Get(id);
            if (item is EquipmentData e)
            {
                return EquipmentYamlFactory.Instance.GetFromData(e);
            }

            return null;
        }
    }
}
