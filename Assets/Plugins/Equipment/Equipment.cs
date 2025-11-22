using GameBase.Modify;
using System.Collections.Generic;

namespace GameBase.Equipments
{
    public enum EquipmentTag : uint
    {

    }
    public class Equipment
    {
        public int ID {  get; set; }
        public EquipmentTag Tag { get; set; }
        public IEquipmentOwner Owner { get; set; }
        public string TextureName { get; set; }
        public int Rarity { get; set; }
        public List<IModifierPair> IntKeyModifiers { get; set; }
        public List<Modifyer> Modifyers { get; set; } = new();

        public void Remove()
        {
            EquipmentSys.Instance.RemoveEntity(this);
        }
    }
}
