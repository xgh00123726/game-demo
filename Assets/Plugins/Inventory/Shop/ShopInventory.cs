using GameBase.Tools;
using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Inventorys
{

    internal struct ShopData
    {
        public int goodID;
        public int weight;
    }    
    /// <summary>
    /// inventory中保存的内容是商店库存index
    /// 需要根据库存index转换成商品id,再通过商品id获取商品信息
    /// </summary>
    public class ShopInventory : CommonInventory<int>
    {
        /// <summary>
        /// datas中保存的是商店的库存信息
        /// </summary>
        private ShopData[] _datas;
        internal int weightSum = 0;

        public void BindFile(string relativePath)
        {
            Init(relativePath);
            Command.Register($"shop-{relativePath}-init", Init);
            Refresh();
        }

        private void Init(string relativePath)
        {
            if (relativePath == null || relativePath.Length == 0 || relativePath == "")
            {
                return;
            }
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/ShopData/{relativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            int len = int.Parse(csvReader[0]);
            _datas = new ShopData[len];
            weightSum = 0;
            for (int i = 0; i < len; i++)
            {
                csvReader.Read();
                int index = int.Parse(csvReader[0]);
                int goodID = int.Parse(csvReader[1]);
                int weight = int.Parse(csvReader[2]);
                int price = int.Parse(csvReader[3]);

                _datas[i] = new ShopData()
                {
                    goodID = goodID,
                    weight = weight,
                };
                weightSum += weight;
            }

            reader.Close();
        }

        /// <summary>
        /// 根据随机数和权重获取一个随机的商品ID下标，随机数范围[0,weightSum)
        /// </summary>
        /// <param name="randomInt"></param>
        /// <returns></returns>
        private int RandomToGoodIndex(int randomInt)
        {
            if (randomInt < 0 || _datas.Length == 0)
            {
                return -1;
            }

            for (int i = 0; i < _datas.Length; i++)
            {
                randomInt -= _datas[i].weight;
                if (randomInt < 0)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 获取商店中第index位置的商品信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ShopItemInfo GetItemInfoFromShoppingPosition(int index)
        {
            var shopDataIndex = this[index];
            var goodID = _datas[shopDataIndex].goodID;
            return ShopDataBase.Get(goodID);
        }

        /// <summary>
        /// 获取商品库存的第index个物品的信息
        /// </summary>
        /// <param name="shopDataIndex"></param>
        /// <returns></returns>
        public ShopItemInfo GetItemInfoFromShopDataIndex(int shopDataIndex)
        {
            var goodID = _datas[shopDataIndex].goodID;
            return ShopDataBase.Get(goodID);
        }
         
        /// <summary>
        /// 将当前商店的商品信息打乱
        /// </summary>
        public void Refresh()
        {
            for (int i = 0; i < Size; ++i)
            {
                int randInt = Random.Range(0, weightSum - 1);
                int shopIndex = RandomToGoodIndex(randInt);
                this[i] = shopIndex;
            }
        }
    }
}
