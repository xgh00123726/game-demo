using GameBase.Inventorys;
using GameBase.Tools;
using NReco.Csv;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
namespace Instance.MVC
{
    public class InventoryDataBase : IDataBase<InventoryData>
    {
        private InventoryData[] _datas;
        private static InventoryDataBase _instance = new();
        protected InventoryDataBase()
        {
            if (RelativePath == null || RelativePath.Length == 0 || RelativePath == "")
            {
                return;
            }
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/{RelativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            _datas = new InventoryData[int.Parse(csvReader[0])];
            for (int i = 0; i < _datas.Length; i++)
            {
                csvReader.Read();

                var data = new InventoryData();
                data.ID = int.Parse(csvReader[0]);
                Enum.TryParse(csvReader[1], out data.tag);
                data.IconTextureID = int.Parse(csvReader[2]);
                data.buffID = int.Parse(csvReader[3]);
                data.spellActionModifyerID = int.Parse(csvReader[4]);

                _datas[i] = data;
            }

            reader.Close();
        }

        public static InventoryDataBase Instance => _instance;

        public string RelativePath => "Instance/Inventory/CommonInventory/CommonDataBase.csv";

        public int Count => _datas.Length;

        public InventoryData Read(int index)
        {
            return _datas[index];
        }

        public void Write(InventoryData data, int index)
        {
            _datas[index] = data;
        }
    }
}
