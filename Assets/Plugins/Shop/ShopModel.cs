using GameBase.Tools;
using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace GameBase.Shops
{
    public struct ShopModelData
    {
        public int goodID;
        public int weight;
        public int price;
    }

    public class ShopModel
    {
        internal int goodNums = 5;
        internal AutoFillList<int> currGoodIndexInModel = new(); // 当前商店物品的信息在model中的下标

        internal List<ShopModelData> datas = new(); // 物品id对应的信息
        internal int weightSum = 0;

        internal ShopModel(string relativePath)
        {
            Init(relativePath);
            Command.Register($"shop-{relativePath}-init", Init);
            GoodNums = 5;
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
            weightSum = 0;
            for (int i = 0; i < len; i++)
            {
                csvReader.Read();
                int index = int.Parse(csvReader[0]);
                int goodID = int.Parse(csvReader[1]);
                int weight = int.Parse(csvReader[2]);
                int price = int.Parse(csvReader[3]);

                datas.Add(new ShopModelData()
                {
                    goodID = goodID,
                    weight = weight,
                    price = price,
                });
                weightSum += weight;
            }

            reader.Close();
        }

        /// <summary>
        /// 获取index位置的商品id
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetGoodID(int index)
        {
            var dataIndex = currGoodIndexInModel[index];
            if (dataIndex < 0)
            {
                return -1;
            }
            return datas[dataIndex].goodID;
        }

        public int GoodNums
        {
            get => goodNums;
            set
            {
                currGoodIndexInModel.Resize(value, -1);
                goodNums = value;
            }
        }

        /// <summary>
        /// 根据随机数和权重获取一个随机的商品ID下标，随机数范围[0,weightSum)
        /// </summary>
        /// <param name="randomInt"></param>
        /// <returns></returns>
        private int RandomToGoodIndex(int randomInt)
        {
            if (randomInt < 0 || datas.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < datas.Count; i++)
            {
                randomInt -= datas[i].weight;
                if (randomInt < 0)
                {
                    return i;
                }
            }

            return -1;
        }

        protected internal virtual bool Purchase(int index, IShoper shoper)
        {
            if (index < 0 || index >= goodNums)
            {
                return false;
            }
            var modelIndex = currGoodIndexInModel[index];

            if (modelIndex < 0)
            {
                return false;
            }

            var price = datas[modelIndex].price;
            if (shoper.Gold < price)
            {
                return false;
            }

            return true;
        }

        protected internal virtual void RemoveItem(int index)
        {
            currGoodIndexInModel[index] = -1;
        }

        internal void Refresh()
        {
            for (int i = 0; i < goodNums; ++i)
            {
                int randInt = Random.Range(0, weightSum - 1);
                int index = RandomToGoodIndex(randInt);
                currGoodIndexInModel[i] = datas[index].goodID;
            }
        }

        public bool HasItem(int index)
        {
            return currGoodIndexInModel[index] >= 0;
        }
    }
}
