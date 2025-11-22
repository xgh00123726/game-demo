using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.AI
{
    public abstract class BaseAI
    {
        protected internal Creature owner;
        internal bool enable = true;
        internal float enableRecoverTime;

        public void Enable()
        {
            enable = true;
        }

        public void Disable(float time = 3)
        {
            enable = false;
            if (time > 0)
            {
                enableRecoverTime = Time.time + time;
            }
        }

        internal protected abstract void Update();

        public void AddTo(Creature c)
        {
            owner = c;
            OnAddTo(c);
            c.AI = this;
        }

        protected abstract void OnAddTo(Creature c);
    }
}
