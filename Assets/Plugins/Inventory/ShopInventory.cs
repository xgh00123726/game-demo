using GameBase.Tools;
using UnityEngine;

namespace GameBase.Inventorys
{
    public class ShopInventory : CommonInventory<int>
    {
        private WeightInventory<int> _backupStore;

        public ShopInventory(string relativePath)
        {
            Init(relativePath);
            Refresh();
        }

        private void Init(string relativePath)
        {
            _backupStore = new(new CsvReaderReflect<WeightInventory<int>.WeightedType>()
                .Parse($"{Application.streamingAssetsPath}/ShopData/{relativePath}"));
        }

         
        /// <summary>
        /// 将当前商店的商品信息打乱
        /// </summary>
        public void Refresh()
        {
            for (int i = 0; i < Size; ++i)
            {
                _backupStore.GetRandom(out int goodID);
                this[i] = goodID;
            }
        }
    }
}
