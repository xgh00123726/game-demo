using GameBase.Inventorys;
using GameBase.Texts;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;
using GameBase.Tools;
using System.Collections.Generic;
using GameBase.Creatures;

namespace Instance
{
    public enum InventoryItemType
    {
        None = 0,
        Equipment,
        SpellActionModifier,
    }
    public struct InventoryData
    {
        public InventoryItemType type;
        public int reflectedID;
    }
    public class InventoryDataBase : CsvDataBase<InventoryData, InventoryDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("CommonDataBase.csv");

        private Dictionary<InventoryData, int> _inventoryIDDict = new();

        public InventoryDataBase()
        {
            for (int i = 0; i < _datas.Length; i++)
            {
                var data = _datas[i];
                _inventoryIDDict.Add(data, i);
            }
        }

        public int GetIndex(InventoryData data)
        {
            if (!_inventoryIDDict.ContainsKey(data))
            {
                return -1;
            }

            return _inventoryIDDict[data];
        }
    }
}
