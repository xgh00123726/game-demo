using GameBase.Inventorys;
using GameBase.Texts;
using NReco.Csv;
using System;
using System.IO;
using UnityEngine;
using GameBase.Tools;

namespace Instance
{
    public class CommonInventoryDataBase
    {
        private static CommonInventoryData[] _datas;

        static CommonInventoryDataBase()
        {
            _datas = new CsvReaderReflect<CommonInventoryData>()
                .Parse($"{Application.streamingAssetsPath}/Instance/Inventory/CommonInventory/CommonDataBase.csv");
            //Init("Instance/Inventory/CommonInventory/CommonDataBase.csv");
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
                    secondID = int.Parse(csvReader[2])
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
