using GameBase.Creatures;
using GameBase.EntitySystem;

namespace GameBase.AI
{
    public abstract class BaseAI : IPoolable
    {
        internal protected abstract void Update();
        void IPoolable.AfterGet()
        {
            AISys.Instance.actions.AddLast(Update);
        }

        void IPoolable.BeforeRelease()
        {
            AISys.Instance.actions.Remove(Update);
        }

        public abstract void AddTo(Creature c);
    }
}
