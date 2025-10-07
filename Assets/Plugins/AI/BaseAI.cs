using GameBase.Creatures;
using GameBase.EntitySystem;

namespace GameBase.AI
{
    public abstract class BaseAI : IPoolable
    {
        internal protected Creature owner;
        internal protected abstract void Update();
        void IPoolable.AfterGet()
        {
            AISys.Instance.AIList.AddLast(this);
        }

        void IPoolable.BeforeRelease()
        {
            AISys.Instance.AINeedRemove.AddLast(this);
        }

        public void AddTo(Creature c)
        {
            owner = c;
            OnAddTo(c);
        }

        protected abstract void OnAddTo(Creature c);
    }
}
