namespace GameBase.Tools
{
    public class BehaviorTree
    {
        internal Behavior _root;
        internal int tickRate = 0;
        internal int currentTick = 0;
        internal int tickCount = 0;

        public BehaviorTree(Behavior root)
        {
            _root = root;
        }

        public void Tick()
        {
            ++tickCount;
            if (++currentTick >= tickRate)
            {
                currentTick = 0;
                _root.Tick();
            }
        }
    }
}
