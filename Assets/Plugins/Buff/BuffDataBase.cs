using GameBase.Modify;
using GameBase.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Buffs
{
    public enum BuffType
    {
        Common,
        Equipment,
    }
    public struct BuffInfo
    {
        public BuffType type;
        public int buffModifiersID;
        public float duration;
        public string textureName;
        public int rarity;
    }
    public class BuffDataBase : CsvDataBase<BuffInfo, BuffDataBase>
    {
        internal static List<Dictionary<int, float>> datas = new();

        protected override string DataBasePath => CsvDataBasePath.DefaultFolder("BuffInfoDataBase.csv");

        static BuffDataBase()
        {
            Init();
            Command.Register($"ReloadBuffData", Init);
        }

        private static void Init()
        {
            ParseDataFromJson(CsvDataBasePath.DefaultFolder("BuffDataBase.json"), datas);
        }

        private static void ParseDataFromJson(string path, List<Dictionary<int, float>> data)
        {
            StreamReader reader = File.OpenText(path);
            JsonTextReader jReader = new JsonTextReader(reader);
            JObject jObj = (JObject)JToken.ReadFrom(jReader);

            foreach (var item in jObj)
            {
                JObject buffIns = (JObject)item.Value;
                Dictionary<int, float> buffData = new();
                foreach (var kv in buffIns)
                {
                    int modifyID = ModifyTable.GetID(kv.Key);

                    buffData.Add(modifyID, float.Parse(kv.Value.ToString()));
                }
                datas.Add(buffData);
            }
            jReader.Close();
            reader.Close();
        }
    }
}
