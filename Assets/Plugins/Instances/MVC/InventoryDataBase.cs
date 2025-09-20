using GameBase.Inventorys;
using GameBase.Texts;
using GameBase.Tools;
using NReco.Csv;
using System;
using GameBase.UI.MVC;
using System.IO;
using UnityEngine;
namespace Instance.UI.MVC
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
                Enum.TryParse(csvReader[4], out data.spellActionModifierType);
                data.spellActionModifyerID = int.Parse(csvReader[5]);

                _datas[i] = data;
            }

            reader.Close();

            TextMgr.Init(TextFileName);
        }

        public static InventoryDataBase Instance => _instance;

        public string RelativePath => "Instance/Inventory/CommonInventory/CommonDataBase.csv";

        public string TextFileName => "CommonDataBase.csv";

        public int Count => _datas.Length;

        public string GetText(int index, params string[] args)
        {
            return TextMgr.Get(TextFileName, index, args);
        }

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
