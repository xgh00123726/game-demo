using GameBase.Modify;

namespace GameBase.Buffs
{
    public interface IBuffOwner : IModifyOwner<float>
    {
        BuffContainer Buffs { get; }
        public void AddBuff(Buff e)
        {
            Buffs.AddBuff(e);
            e.owner = this;

            foreach (var em in e.modifyers.FixedModifyers)
            {
                e.owner.Modifyables.AddModify(em.Key, em.Value);
            }
            foreach (var em in e.modifyers.SetModifyers)
            {
                e.owner.Modifyables.AddModify(em.Key, em.Value);
            }
            foreach (var em in e.modifyers.CurrModifyers)
            {
                e.owner.Modifyables.AddModify(em.Key, em.Value);
            }
        }

        public void RemoveBuff(Buff e)
        {
            BuffSys.Instance.RemoveBuff(e);
        }
    }
}
