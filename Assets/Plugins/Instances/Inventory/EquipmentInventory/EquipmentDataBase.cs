using GameBase.Inventorys;
using System.Collections.Generic;
namespace Instance.Inventory
{
    public class EquipmentDataBase : IDataBase<EquipmentItem>
    {
        void IDataBase<EquipmentItem>.Read(out EquipmentItem data)
        {
            data = new EquipmentItem();
        }

        void IDataBase<EquipmentItem>.Read(out IEnumerator<EquipmentItem> datas)
        {
            datas = null;
        }

        void IDataBase<EquipmentItem>.Write(EquipmentItem data)
        {
        }

        void IDataBase<EquipmentItem>.Write(IEnumerator<EquipmentItem> datas)
        {
        }
    }
}
