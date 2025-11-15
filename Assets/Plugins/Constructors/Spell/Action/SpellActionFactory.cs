using GameBase.EntitySystem;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class SpellActionFactory : MultiFactory<ISpellAction, SpellActionFactory>
    {
        public SpellActionFactory()
        {
            Register(MShotYamlFactory.Instance.Get);
            Register(MTrigYamlFactory.Instance.Get);
        }
    }
}
