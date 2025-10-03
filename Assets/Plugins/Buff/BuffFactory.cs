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

    public class BuffFactory
    {
        static BuffFactory()
        {

        }

        /// <summary>
        /// ¥”info idªÒ»°buff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Buff Get(int id)
        {
            var info = BuffDataBase.Instance[id];
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
            if (id < 0 || id >= BuffDataBase.datas.Count)
            {
                return null;
            }
            var e = BuffSys.Instance.NewEntity();
            e.id = id;
            var buffData = BuffDataBase.datas[id];
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
