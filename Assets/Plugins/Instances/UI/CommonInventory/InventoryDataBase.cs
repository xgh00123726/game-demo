using GameBase.Inventorys;
using GameBase.Texts;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;
using GameBase.Tools;

namespace Instance
{
    public enum InventoryType
    {
        None = 0,
        Equipment,
        SpellActionModifier,
    }
    public struct InventoryData
    {
        public InventoryType type;
        public int id;
    }
    public class InventoryDataBase : CsvDataBase<InventoryData, InventoryDataBase>
    {
        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("CommonDataBase.csv");
    }
}
