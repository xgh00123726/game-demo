using GameBase.Tools;

namespace GameBase.Equipments
{
    public struct EquipmentName
    {
        public string name;
    }
    public class EquipmentNameDataBase : CsvDataBase<EquipmentName, EquipmentNameDataBase> { }
}
