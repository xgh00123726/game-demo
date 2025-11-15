using GameBase.Creatures;
using GameBase.EntitySystem;

namespace Constructor.Creatures
{
    public class CreatureFactory : MultiFactory<Creature, CreatureFactory>
    {
        public CreatureFactory()
        {
            Register(CreatureYamlFactory.Instance.Get);
        }
    }
}
