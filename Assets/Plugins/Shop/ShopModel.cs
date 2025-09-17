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
        internal AutoFillList<int> currGoodIDs = new();
        internal List<ShopModelData> datas = new();
        internal int weightSum = 0;

        internal ShopModel(string relativePath)
        {
            Init(relativePath);
            Command.Register($"shop-{relativePath}-init", Init);
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

        public int GetGoodID(int index)
        {
            return datas[index].goodID;
        }

        internal int GoodNums
        {
            get => goodNums;
            set
            {
                currGoodIDs.Resize(value, -1);
                goodNums = value;
            }
        }

        /// <summary>
        /// 根据随机数和权重获取一个随机的商品ID下标，随机数范围[0,weightSum)
        /// </summary>
        /// <param name="randomInt"></param>
        /// <param name="weightSum"></param>
        /// <param name="goodIDs"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        private int RandomToGoodIndex(int randomInt)
        {
            if (randomInt <= 0 || datas.Count == 0)
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
            XLogger.Instance.Log($"purchase:{index}, l:{datas.Count}, ");

            if (index < 0 || index >= goodNums)
            {
                return false;
            }
            if (shoper.Gold < datas[index].price)
            {
                return false;
            }

            currGoodIDs[index] = -1;
            return true;
        }

        internal void Refresh()
        {
            for (int i = 0; i < goodNums; ++i)
            {
                int randInt = Random.Range(0, weightSum - 1);
                int index = RandomToGoodIndex(randInt);
                currGoodIDs[i] = datas[index].goodID;
            }
        }
    }
}
