using GameBase.Inventorys;
using GameBase.Texts;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;

namespace Instance
{
    public class CommonInventoryDataBase
    {
        private static CommonInventoryData[] _datas;

        static CommonInventoryDataBase()
        {
            Init("Instance/Inventory/CommonInventory/CommonDataBase.csv");
        }

        private static void Init(string relativePath)
        {
            if (relativePath == null || relativePath.Length == 0 || relativePath == "")
            {
                return;
            }
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/{relativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            _datas = new CommonInventoryData[int.Parse(csvReader[0])];
            for (int i = 0; i < _datas.Length; i++)
            {
                csvReader.Read();

                _datas[i] = new CommonInventoryData()
                {
                    id = int.Parse(csvReader[0]),
                    secondID = int.Parse(csvReader[2]),
                    iconTextureID = int.Parse(csvReader[3]),
                    rarity = int.Parse(csvReader[4])
                };
                Enum.TryParse(csvReader[1], out _datas[i].type);
            }

            reader.Close();
        }

        public static int Count => _datas.Length;

        public static CommonInventoryData Get(int index)
        {
            return _datas[index];
        }
    }
}
