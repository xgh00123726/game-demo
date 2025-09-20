using GameBase.Tools;

public class TimerBehavior : Decorator
{
    private int _interriputPeriod;
    private BehaviorTree _tree;
    public TimerBehavior(int interruptPeriod, BehaviorTree tree)
    {
        _interriputPeriod = interruptPeriod;
        _tree = tree;
    }

    protected override Status OnUpdate()
    {
        if (_tree.tickCount % _interriputPeriod == 0)
        {
            child.Tick();
            if (child.IsSuccess) return Status.Success;
            if (child.IsFailure) return Status.Failure;
            return Status.Running;
        }

        return Status.Failure;
    }
}
