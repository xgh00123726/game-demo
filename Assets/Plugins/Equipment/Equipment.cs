using GameBase.Modify;
using System.Collections.Generic;

namespace GameBase.Equipments
{
    public enum EquipmentTag : uint
    {

    }
    public class Equipment
    {
        public EquipmentTag tag;
        public IEquipmentOwner owner;
        public string textureName;
        public int rarity;
        public List<KeyValuePair<int, float>> iModifiers;
        public List<Modifyer> modifyers = new();

        public void Remove()
        {
            EquipmentSys.Instance.RemoveEntity(this);
        }
    }
}
