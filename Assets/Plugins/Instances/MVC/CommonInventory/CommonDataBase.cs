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
    public class CommonDataBase : IDataBase<CommonItemData>
    {
        private CommonItemData[] _datas;
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
            _datas = new CommonItemData[int.Parse(csvReader[0])];
            for (int i = 0; i < _datas.Length; i++)
            {
                csvReader.Read();

                var data = new CommonItemData();
                int index = int.Parse(csvReader[0]);
                Enum.TryParse(csvReader[1], out InventoryTag tag);
                data.Tag = tag;
                data.iconTextureID = int.Parse(csvReader[2]);
                data.buffID = int.Parse(csvReader[3]);
                data.spellActionModifyerID = int.Parse(csvReader[4]);
                data.ID = index;

                _datas[i] = data;
            }

            reader.Close();
        }

        public static CommonDataBase Instance => _instance;

        public string RelativePath => "Instance/Inventory/CommonInventory/CommonDataBase.csv";

        int IDataBase<CommonItemData>.Count => _datas.Length;

        CommonItemData IDataBase<CommonItemData>.Read(int index)
        {
            return _datas[index];
        }

        void IDataBase<CommonItemData>.Write(CommonItemData data, int index)
        {
            _datas[index] = data;
        }
    }
}
