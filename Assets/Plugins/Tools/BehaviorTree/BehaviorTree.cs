namespace GameBase.Tools
{
    public class BehaviorTree
    {
        internal Behavior _root;
        internal int tickRate;
        internal int currentTick;
        public BehaviorTree(Behavior root)
        {
            _root = root;
        }

        public void Tick()
        {
            if (++currentTick >= tickRate)
            {
                currentTick = 0;
                _root.Tick();
            }
        }
    }
}
