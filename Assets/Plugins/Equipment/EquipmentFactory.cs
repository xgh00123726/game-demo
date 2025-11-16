using GameBase.EntitySystem;

namespace GameBase.Equipments
{
    public class EquipmentFactory : MultiFactory<Equipment, EquipmentFactory>
    {
        public EquipmentFactory()
        {
            Register(EquipmentYamlFactory.Instance.Get);
        }

        public string GetName(int id)
        {
            if (id >= EquipmentNameDataBase.Instance.Size)
            {
                return null;
            }
            return EquipmentNameDataBase.Instance[id].name;
        }

        public Equipment GetByID(int id)
        {
            return EquipmentYamlFactory.Instance.GetFromData(GetData(id));
        }

        public EquipmentData GetData(int id)
        {
            if (id >= EquipmentNameDataBase.Instance.Size)
            {
                return null;
            }

            return EquipmentYamlFactory.Instance.GetData(GetName(id));
        }
    }
}
