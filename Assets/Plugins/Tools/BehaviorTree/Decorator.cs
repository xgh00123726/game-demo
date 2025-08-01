using UnityEngine;

namespace GameBase.Tools
{
    public abstract class Decorator : Behavior
    {
        protected Behavior child;
        public override void AddChild(Behavior child)
        {
            this.child = child;
        }
    }
}
