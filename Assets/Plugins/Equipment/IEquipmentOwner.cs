using GameBase.Modify;

namespace GameBase.Equipments
{
    public interface IEquipmentOwner : IModifieder
    {
        void OnGetEquipment(Equipment equip);
        void OnRemoveEquipment(Equipment equip);
    }
}
