using GameBase.EntitySystem;
using GameBase.Items;
using GameBase.Tools;

namespace GameBase.Buffs
{
    public class BuffFactory : MultiFactory<Buff, BuffFactory>
    {
        public BuffFactory()
        {
            Register(BuffYamlFactory.Instance.Get);
        }

        public Buff Get(int id)
        {
            var item = ItemDataMgr.Get(id);
            if (item is BuffData b)
            {
                return BuffYamlFactory.Instance.GetFromData(b);
            }

            return null;
        }
    }
}
