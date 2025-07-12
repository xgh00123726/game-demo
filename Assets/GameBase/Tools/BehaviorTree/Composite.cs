using System.Collections.Generic;

namespace GameBase.Tools
{
    public abstract class Composite : Behavior
    {
        protected LinkedList<Behavior> children = new LinkedList<Behavior>();

        public override void AddChild(Behavior child)
        {
            children.AddLast(child);
        }
        public virtual void Clear()
        {
            children.Clear();
        }
    }
}
