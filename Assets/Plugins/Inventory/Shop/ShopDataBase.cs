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
            Init();
        }

        private static void Init()
        {
            _datas = new CsvReaderReflect<ShopItemInfo>()
                .Parse($"{Application.streamingAssetsPath}/ShopData/ShopItemInfos.csv");
        }

        public static ShopItemInfo Get(int goodID)
        {
            return _datas[goodID];
        }
    }
}
