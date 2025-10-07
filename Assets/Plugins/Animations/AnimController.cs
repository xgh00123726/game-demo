using GameBase.Creatures;

namespace GameBase.Animations
{
    public abstract class AnimController
    {
        internal protected Creature owner;
        public void AddTo(Creature creature)
        {
            owner = creature;
            OnAddTo(creature);
        }
        public abstract void OnAddTo(Creature creature);
        public virtual void Update() { }
    }
}
