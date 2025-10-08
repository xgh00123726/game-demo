using GameBase.Creatures;
using GameBase.EntitySystem;

namespace GameBase.AI
{
    public abstract class BaseAI
    {
        protected internal Creature owner;
        internal protected abstract void Update();

        public void AddTo(Creature c)
        {
            owner = c;
            OnAddTo(c);
            c.ai = this;
        }

        protected abstract void OnAddTo(Creature c);
    }
}
