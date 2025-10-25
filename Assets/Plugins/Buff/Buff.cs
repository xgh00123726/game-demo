using GameBase.Modify;
using GameBase.Tools;
using System;
using GameBase.EntitySystem;
using UnityEngine;
using System.Collections.Generic;

namespace GameBase.Buffs
{
    public class Buff
    {
        internal float durationRemain;
        internal float instantiateTime;
        internal int stackNum = 1;
        internal bool alive;
        internal IBuffOwner owner;
        internal bool isInfiDuration;

        public int id;
        public float durationSet;
        public List<Modifyer> modifyers = new();

        public bool ALive => alive;
        public float DurationRemain => durationRemain;


        public void AddTo(IBuffOwner owner, float duration = -1)
        {
            this.owner = owner;

            var info = BuffDataBase.Instance[id];
            int modifiersID = info.buffModifiersID;
            var modifierDict = BuffDataBase.datas[modifiersID];
            foreach (var kvp in modifierDict)
            {
                int modifyKey = kvp.Key;
                float value = kvp.Value;
                var m = ModifyerSys.Instance.NewEntity();
                m.value = value;
                m.type = ModifyType.Temporary | ModifyType.Always;
                m.AddTo(owner.Modifyables[modifyKey]);
                modifyers.Add(m);
            }

            if (duration > 0)
            {
                this.durationSet = duration;
                this.durationRemain = duration;
            }

            owner.OnGetBuff(this);
        }

        public void Remove()
        {
            BuffSys.Instance.RemoveEntity(this);
        }
    }
}
