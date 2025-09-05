using GameBase.Buffs;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;

namespace Constructor.Spells.Action
{
    public struct BuffSelfData
    {
        public Buffs.Type buffType;
        public int buffID;
        public float duration;
    }

    public class BuffSelfAction : IAction
    {
        public BuffSelfData data;
        void IAction.CastAction(Spell spell)
        {
            var bOwner = spell.speller as IBuffOwner;
            if (bOwner == null)
            {
                XLogger.Instance.Log("spell must be buff owner");
                return;
            }

            var eb = Buffs.Factory.Instance.Get(data.buffType, data.buffID);
            eb.durationSet = data.duration;
            eb.uiStyle = UIStyle.Buff;
            bOwner.AddBuff(eb);
        }
    }

    public class BuffSelf : BaseConstructor<BuffSelfData, BuffSelfAction, BuffSelf>
    {
        protected override string RelativePath => "Spell/Action/BuffSelf.csv";

        protected override BuffSelfAction Get()
        {
            return new BuffSelfAction();
        }

        protected override void Parse(CsvReader line, ref BuffSelfData data)
        {
            Enum.TryParse(line[1], out data.buffType);
            data.buffID = int.Parse(line[2]);
            data.duration = float.Parse(line[3]);
        }

        protected override void Set(BuffSelfAction e, in BuffSelfData data)
        {
            e.data = data;
        }
    }
}
