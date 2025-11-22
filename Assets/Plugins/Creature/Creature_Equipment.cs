using GameBase.Buffs;
using GameBase.Equipments;
using GameBase.Inventorys;
using System.Xml.Linq;

namespace GameBase.Creatures
{
    public partial class Creature : IEquipmentOwner
    {
        public CommonInventory<Equipment> Equipments { get; set; } = new() { Size = 6 };

        public bool AddEquipmentByID(int id, int index)
        {
            if (Equipments.HasItem(index))
            {
                return false;
            }

            var equipment = EquipmentFactory.Instance.Get(id);
            if (equipment == null)
            {
                return false;
            }

            Equipments.Add(equipment, index);
            equipment.Owner = this;

            return true;
        }

        public bool AddEquipment(string name, int index)
        {
            if (Equipments.HasItem(index))
            {
                return false;
            }

            var equipment = EquipmentFactory.Instance.Get(name);
            Equipments.Add(equipment, index);
            equipment.Owner = this;

            return true;
        }

        public void RemoveEquipment(int index)
        {
            if (!Equipments.HasItem(index))
            {
                return;
            }
            Equipments[index].Remove();
            Equipments.Remove(index);
        }

        public Equipment GetEquipment(int index)
        {
            if (Equipments.HasItem(index))
            {
                return Equipments[index];
            }

            return null;
        }

        public void SwapEquipment(int p1, int p2)
        {
            Equipments.Swap(p1, p2);
        }

        void IEquipmentOwner.OnGetEquipment(Equipment equip)
        {
        }

        void IEquipmentOwner.OnRemoveEquipment(Equipment equip)
        {
        }
    }
}
