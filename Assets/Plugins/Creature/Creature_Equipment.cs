using GameBase.Buffs;
using GameBase.Inventorys;
using GameBase.Tools;

namespace GameBase.Creatures
{
    public partial class Creature
    {
        public CommonInventory<Buff> equipments = new() { Size = 6 };

        public bool AddEquipment(int id, int index)
        {
            if (equipments.HasItem(index))
            {
                return false;
            }

            var info = BuffDataBase.Instance[id];
            if (info.type == BuffType.Equipment)
            {
                var buff = BuffFactory.Get(id);
                buff.AddTo(this);
                equipments[index] = buff;
                return true;
            }

            return false;
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

        public Buff GetEquipment(int index)
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
    }
}
