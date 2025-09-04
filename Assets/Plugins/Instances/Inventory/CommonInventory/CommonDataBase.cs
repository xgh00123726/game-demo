using GameBase.Inventorys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Instance.Inventory
{
    public class CommonDataBase : IDataBase<CommonItem>
    {
        void IDataBase<CommonItem>.Read(out CommonItem data)
        {
            data = new CommonItem();
        }

        void IDataBase<CommonItem>.Read(out IEnumerator<CommonItem> datas)
        {
            datas = null;
        }

        void IDataBase<CommonItem>.Write(CommonItem data)
        {
        }

        void IDataBase<CommonItem>.Write(IEnumerator<CommonItem> datas)
        {
        }
    }
}
