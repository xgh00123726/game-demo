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
        private static CommonInventoryDataBase _instance = new();
        private static string textFileName = "CommonDataBase.csv";

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
                    ID = int.Parse(csvReader[0]),
                    iconTextureID = int.Parse(csvReader[2]),
                    buffID = int.Parse(csvReader[3]),
                    spellActionModifyerID = int.Parse(csvReader[5]),
                    rarity = int.Parse(csvReader[6])
                };
                Enum.TryParse(csvReader[1], out _datas[i].tag);
                Enum.TryParse(csvReader[4], out _datas[i].spellActionModifierType);
            }

            reader.Close();

            TextMgr.Init(textFileName);
        }

        public static int Count => _datas.Length;

        public static string GetText(int index, params string[] args)
        {
            return TextMgr.Get(textFileName, index, args);
        }
        public static CommonInventoryData Get(int index)
        {
            return _datas[index];
        }
    }
}
