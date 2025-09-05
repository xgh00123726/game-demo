using GameBase.Inventorys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Instance.Inventory
{
    public class CommonDataBase : IDataBase<CommonItemData>
    {
        void IDataBase<CommonItemData>.Read(out CommonItemData data)
        {
            data = new CommonItemData();
        }

        void IDataBase<CommonItemData>.Read(out IEnumerator<CommonItemData> datas)
        {
            datas = null;
        }

        void IDataBase<CommonItemData>.Write(CommonItemData data)
        {
        }

        void IDataBase<CommonItemData>.Write(IEnumerator<CommonItemData> datas)
        {
        }
    }
}
