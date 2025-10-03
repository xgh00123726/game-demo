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
        public int iconTextureID;
        public int rarity;
    }
    public struct BuffData
    {
        public float duration;
        public int fixedValue;
        public int currentPercent;
        public int setPercent;
    }
    public class BuffDataBase : CsvDataBase<BuffInfo, BuffDataBase>
    {
        internal static List<Dictionary<int, BuffData>> datas = new();

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

        private static void ParseDataFromJson(string path, List<Dictionary<int, BuffData>> data)
        {
            StreamReader reader = File.OpenText(path);
            JsonTextReader jReader = new JsonTextReader(reader);
            JObject jObj = (JObject)JToken.ReadFrom(jReader);

            foreach (var item in jObj)
            {
                JObject buffIns = (JObject)item.Value;
                Dictionary<int, BuffData> buffData = new();
                foreach (var kv in buffIns)
                {
                    var modifyKey = kv.Key;
                    var modifyValues = kv.Value;
                    int modifyID = ModifyTable.GetID(modifyKey);
                    BuffData itemData = new();

                    if (modifyValues["setPer"] != null)
                    {
                        itemData.setPercent = int.Parse(modifyValues["setPer"].ToString());
                    }
                    else
                    {
                        itemData.setPercent = int.MinValue;
                    }
                    if (modifyValues["fixed"] != null)
                    {
                        itemData.fixedValue = int.Parse(modifyValues["fixed"].ToString());
                    }
                    else
                    {
                        itemData.fixedValue = int.MinValue;
                    }
                    if (modifyValues["currPer"] != null)
                    {
                        itemData.currentPercent = int.Parse(modifyValues["currPer"].ToString());
                    }
                    else
                    {
                        itemData.currentPercent = int.MinValue;
                    }
                    buffData.Add(modifyID, itemData);
                }
                datas.Add(buffData);
            }
            jReader.Close();
            reader.Close();
        }
    }
}
