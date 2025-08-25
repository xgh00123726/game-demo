using GameBase.Buffs;
using GameBase.EntitySystem;
using GameBase.Tools;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GameBase.Modify;

namespace Constructor.Buffs
{
    public class Common
    {
        public struct ModifyData
        {
            public float duration;
            public int fixedValue;
            public int currentPercent;
            public int setPercent;
        }

        private static Common _instance;
        public static Common Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Common();
                    _instance.Init();
                    Command.Register($"{typeof(Common).FullName}-init", _instance.Init);
                }
                return _instance;
            }
        }

        protected string RelativePath => "Buffs/Common.json";
        private List<Dictionary<int, ModifyData>> _datas = new();

        private void Init()
        {
            ParseFromJson(RelativePath, _datas);
        }

        private void ParseFromJson(string path, List<Dictionary<int, ModifyData>> data)
        {
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/ConstructorData/{RelativePath}");
            JsonTextReader jReader = new JsonTextReader(reader);
            JObject jObj = (JObject)JToken.ReadFrom(jReader);

            foreach (var item in jObj)
            {
                JObject buffIns = (JObject)item.Value;
                Dictionary<int, ModifyData> buffData = new();
                foreach (var kv in buffIns)
                {
                    var modifyKey = kv.Key;
                    var modifyValues = kv.Value;
                    int modifyID = ModifyableContainer<float>.GetIDOfKey(modifyKey);
                    ModifyData itemData = new();

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

        public Buff Get(int id)
        {
            if (id < 0 || id >= _datas.Count)
            {
                return null;
            }
            var e = BuffSys.Instance.NewEntity<Buff>();
            var buffData = _datas[id];
            foreach (var mData in buffData)
            {
                var mk = mData.Key;
                var mv = mData.Value;
                if (mv.setPercent != int.MinValue)
                {
                    var ems = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                    ems.ModifyFunc = ConvientModifyerFunc.FloatSetPercent(mv.setPercent);
                    ems.type = ModifyType.Temporary | ModifyType.Aways;
                    ems.duration = 9999;
                    e.modifyers.AddSet(mk, ems);
                }
                if (mv.currentPercent != int.MinValue)
                {
                    var emc = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                    emc.ModifyFunc = ConvientModifyerFunc.FloatCurrPercent(mv.currentPercent);
                    emc.type = ModifyType.Temporary | ModifyType.Aways;
                    emc.duration = 9999;
                    e.modifyers.AddCurr(mk, emc);
                }
                if (mv.fixedValue != int.MinValue)
                {
                    var emf = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                    emf.ModifyFunc = ConvientModifyerFunc.FloatFixedValue(mv.fixedValue);
                    emf.type = ModifyType.Temporary | ModifyType.Aways;
                    emf.duration = 9999;
                    e.modifyers.AddFixed(mk, emf);
                }
            }

            return e;
        }
    }
}
