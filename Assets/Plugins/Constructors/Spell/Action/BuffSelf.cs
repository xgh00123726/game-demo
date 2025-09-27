using GameBase.Buffs;
using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;
using System;

namespace Constructor.Spells.Action
{
    public struct BuffSelfData
    {
        public int buffID;
        public float duration;
    }

    public class BuffSelf : ISpellAction
    {
        public BuffSelfData data;
        bool ISpellAction.CastAction(Spell spell)
        {
            var bOwner = spell.speller as IBuffOwner;
            if (bOwner == null)
            {
                XLogger.Instance.Log("spell must be buff owner");
                return false;
            }

            BuffFactory.Get(data.buffID).AddTo(bOwner, data.duration);

            return true;
        }
    }

    public class BuffSelfCon : BaseConstructor<BuffSelfData, BuffSelf, BuffSelfCon>
    {
        protected override string RelativePath => "Spell/Action/BuffSelf.csv";

        protected override BuffSelf Get()
        {
            return new BuffSelf();
        }

        protected override void Parse(CsvReader line, ref BuffSelfData data)
        {
            data.buffID = int.Parse(line[1]);
            data.duration = float.Parse(line[2]);
        }

        protected override void Set(BuffSelf e, in BuffSelfData data)
        {
            e.data = data;
        }
    }
}
