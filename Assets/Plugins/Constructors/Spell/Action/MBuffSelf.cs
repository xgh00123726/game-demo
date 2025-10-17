using GameBase.Buffs;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.EntitySystem;
using GameBase.Creatures;

namespace Constructor.Spells.Action
{
    public struct MBuffSelfData
    {
        public int buffID;
        public float duration;
    }

    public class MBuffSelf : ModifyableAction
    {
        public MBuffSelfData data;
        protected override bool CastAction(Spell spell, in ModifyableModifyData modifyData)
        {
            if (spell.speller is Creature c)
            {
                c.AddBuff(data.buffID, data.duration);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class BuffSelfCon : SealedConstructor<MBuffSelfData, MBuffSelf, BuffSelfCon>
    {
        protected override string RelativePath => "Spell/Action/MBuffSelf.csv";

        protected override MBuffSelf GetFromData(in MBuffSelfData data)
        {
            var e = new MBuffSelf();
            e.data = data;
            return e;
        }
    }
}
