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
    public class CommonDataBase : IDataBase<InventoryData>
    {
        private InventoryData[] _datas;
        private static CommonDataBase _instance = new();
        protected CommonDataBase()
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
                data.id = int.Parse(csvReader[0]);
                Enum.TryParse(csvReader[1], out data.tag);
                data.IconTextureID = int.Parse(csvReader[2]);
                data.buffID = int.Parse(csvReader[3]);
                data.spellActionModifyerID = int.Parse(csvReader[4]);

                _datas[i] = data;
            }

            reader.Close();
        }

        public static CommonDataBase Instance => _instance;

        public string RelativePath => "Instance/Inventory/CommonInventory/CommonDataBase.csv";

        int IDataBase<InventoryData>.Count => _datas.Length;

        InventoryData IDataBase<InventoryData>.Read(int index)
        {
            return _datas[index];
        }

        void IDataBase<InventoryData>.Write(InventoryData data, int index)
        {
            _datas[index] = data;
        }
    }
}
