using GameBase.EntitySystem;
using GameBase.Spells;

namespace Constructor.Spells
{
    public class SpellFactory : MultiFactory<Spell, SpellFactory>
    {
        public SpellFactory()
        {
            Register(SpellRegisterFactory.Instance.Get);
            Register(SpellYamlFactory.Instance.Get);
        }
    }
}
