using GameBase.Tools;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;

namespace GameBase.Inventorys
{
    public enum ShopItemType
    {
        InventoryItem,
        Buff
    }
    public struct ShopItemInfo
    {
        public int goodID;
        public ShopItemType type;
        public int secondID;
        public int iconTextureID;
        public int rarity;
        public int price;
    }
    public class ShopDataBase
    {
        private static ShopItemInfo[] _datas;

        static ShopDataBase()
        {
            Init("ShopItemInfos.csv");
        }

        private static void Init(string relativePath)
        {
            if (relativePath == null || relativePath.Length == 0 || relativePath == "")
            {
                return;
            }
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/ShopData/{relativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            int len = int.Parse(csvReader[0]);
            _datas = new ShopItemInfo[len];
            for (int i = 0; i < len; i++)
            {
                csvReader.Read();

                _datas[i] = new ShopItemInfo()
                {
                    goodID = int.Parse(csvReader[1]),
                    secondID = int.Parse(csvReader[3]),
                    iconTextureID = int.Parse(csvReader[4]),
                    rarity = int.Parse(csvReader[5]),
                    price = int.Parse(csvReader[6]),
                };

                Enum.TryParse(csvReader[2], out _datas[i].type);
            }

            reader.Close();
        }

        public static ShopItemInfo Get(int goodID)
        {
            return _datas[goodID];
        }
    }
}
