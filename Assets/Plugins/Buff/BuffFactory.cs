using GameBase.Buffs;
using GameBase.Modify;
using GameBase.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NReco.Csv;
using System;
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
    public class BuffFactory
    {
        private static List<Dictionary<int, BuffData>> _datas = new();
        private static BuffInfo[] _infos;

        static BuffFactory()
        {
            Init();
            Command.Register($"ReloadBuffData", Init);
        }

        private static void Init()
        {
            ParseDataFromJson($"{Application.streamingAssetsPath}/Buffs/BuffData.json", _datas);
            ParseInfoFromCsv($"{Application.streamingAssetsPath}/Buffs/BuffInfo.csv", out _infos);
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
                _datas.Add(buffData);
            }
            jReader.Close();
            reader.Close();
        }

        private static void ParseInfoFromCsv(string path, out BuffInfo[] info)
        {
            info = new CsvReaderReflect<BuffInfo>()
                .Parse(path);
        }

        
        
        
        public static BuffInfo GetInfo(int id)
        {
            return _infos[id];
        }

        /// <summary>
        /// ¥”info idªÒ»°buff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Buff Get(int id)
        {
            var info = _infos[id];
            var buff = GetCommon(id);
            if (info.type == BuffType.Common)
            {
                buff.isInfiDuration = false;
            }
            else if (info.type == BuffType.Equipment)
            {
                buff.isInfiDuration = true;
            }

            return buff;
        }

        private static Buff GetCommon(int id)
        {
            if (id < 0 || id >= _datas.Count)
            {
                return null;
            }
            var e = BuffSys.Instance.NewEntity();
            e.id = id;
            var buffData = _datas[id];
            foreach (var mData in buffData)
            {
                var mk = mData.Key;
                var mv = mData.Value;
                if (mv.setPercent != int.MinValue)
                {
                    var ems = ModifyerSys.Instance.NewEntity();
                    ems.value = mv.setPercent / 100f;
                    ems.type = ModifyType.Temporary | ModifyType.Aways;
                    ems.duration = 9999;
                    e.modifyers.AddSet(mk, ems);
                }
                if (mv.currentPercent != int.MinValue)
                {
                    var emc = ModifyerSys.Instance.NewEntity();
                    emc.value = mv.currentPercent / 100f;
                    emc.type = ModifyType.Temporary | ModifyType.Aways;
                    emc.duration = 9999;
                    e.modifyers.AddCurr(mk, emc);
                }
                if (mv.fixedValue != int.MinValue)
                {
                    var emf = ModifyerSys.Instance.NewEntity();
                    emf.value = mv.fixedValue;
                    emf.type = ModifyType.Temporary | ModifyType.Aways;
                    emf.duration = 9999;
                    e.modifyers.AddFixed(mk, emf);
                }
            }

            return e;
        }


    }
}
