using GameBase.Tools;
using NReco.Csv;
using System;
using System.Data;
using System.IO;
using UnityEngine;

namespace GameBase.Shops
{
    public enum ReflectType
    {
        InventoryItem,
        Buff
    }
    public struct ShopItemInfo
    {
        public int goodID;
        public ReflectType reflectType;
        public int reflectID;
        public int iconTextureID;
        public int rarity;
    }
    public class ShopItemInfos
    {
        private static ShopItemInfo[] _datas;

        static ShopItemInfos()
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
                    reflectID = int.Parse(csvReader[3]),
                    iconTextureID = int.Parse(csvReader[4]),
                    rarity = int.Parse(csvReader[5]),
                };

                Enum.TryParse(csvReader[2], out _datas[i].reflectType);
            }

            reader.Close();
        }

        public static ShopItemInfo Get(int goodID)
        {
            return _datas[goodID];
        }
    }
}
