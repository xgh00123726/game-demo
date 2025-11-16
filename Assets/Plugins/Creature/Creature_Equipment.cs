using GameBase.Buffs;
using GameBase.Equipments;
using GameBase.Inventorys;
using System.Xml.Linq;

namespace GameBase.Creatures
{
    public partial class Creature : IEquipmentOwner
    {
        public CommonInventory<Equipment> equipments = new() { Size = 6 };

        public bool AddEquipmentByID(int id, int index)
        {
            if (equipments.HasItem(index))
            {
                return false;
            }

            var equipment = EquipmentFactory.Instance.GetByID(id);
            equipments.Add(equipment, index);
            equipment.owner = this;

            return true;
        }

        public bool AddEquipment(string name, int index)
        {
            if (equipments.HasItem(index))
            {
                return false;
            }

            var equipment = EquipmentFactory.Instance.Get(name);
            equipments.Add(equipment, index);
            equipment.owner = this;

            return true;
        }

        public void RemoveEquipment(int index)
        {
            if (!equipments.HasItem(index))
            {
                return;
            }
            equipments[index].Remove();
            equipments.Remove(index);
        }

        public Equipment GetEquipment(int index)
        {
            if (equipments.HasItem(index))
            {
                return equipments[index];
            }

            return null;
        }

        public void SwapEquipment(int p1, int p2)
        {
            equipments.Swap(p1, p2);
        }

        void IEquipmentOwner.OnGetEquipment(Equipment equip)
        {
        }

        void IEquipmentOwner.OnRemoveEquipment(Equipment equip)
        {
        }
    }
}
