namespace GameBase.Tools
{
    public class BehaviorTree
    {
        internal Behavior _root;
        internal int tickRate = 0;
        internal int currentSubTick = 0;
        internal int tickCount = 0;

        public BehaviorTree(Behavior root)
        {
            _root = root;
        }

        public void Tick()
        {
            if (++currentSubTick >= tickRate)
            {
                ++tickCount;
                currentSubTick = 0;
                _root.Tick();
            }
        }
    }
}
