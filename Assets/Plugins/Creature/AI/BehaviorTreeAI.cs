using GameBase.Tools;

namespace GameBase.AI
{
    public abstract class BehaviorTreeAI : BaseAI
    {
        protected BehaviorTreeBuilder _builder = new();

        protected internal override void Update()
        {
            _builder.Tree.Tick();
        }
    }
}
