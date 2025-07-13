using System.Diagnostics;
using GameBase.Entity;
using GameBase.Object;
using GameBase.Tools;

public class EnemyGeneric : GameEntity
{
    private BehaviorTreeBuilder behaviorTreeBuilder;
    private int _nearestEntityGetTick = 0;
    private GameEntity _nearestEntity;
    public float warningRange = 5;
    private GameEntity NearestEnemyThisPeriod()
    {
        if (_nearestEntityGetTick == LifeTimeMgr.FixedUpdateTick)
        {
        }
        else
        {
            _nearestEntityGetTick = LifeTimeMgr.FixedUpdateTick;
            _nearestEntity = NearestEntity((GameEntity entity) => entity.camp == Camp.Friendly, warningRange);
        }

        return _nearestEntity;
    }
    protected override void Awake()
    {
        base.Awake();
        camp = Camp.Rival;

        _prefabName = "Enemy1";

        behaviorTreeBuilder = new BehaviorTreeBuilder();
        behaviorTreeBuilder.TickRate(2)
            .Selector()
                .Sequence()
                    .IF(() => infos.HP / attrs.HPMax < 0.3)
                    .IF(() => NearestEnemyThisPeriod() != null)
                    .FF(() => moveComponent.MoveAway(NearestEnemyThisPeriod().transform.position))
                .Back()
                .Sequence()
                    .IF(() => NearestEnemyThisPeriod() != null)
                    .FF(() => moveComponent.MoveTo(NearestEnemyThisPeriod().transform.position))
                .Back()
            .Back()
        .End();     
    }

    private void FixedUpdate()
    {
        behaviorTreeBuilder.Tree.Tick();
    }

    protected override void OnDead()
    {
        base.OnDead();
        Timer.AddTask(1, () =>
        {
            var entity = EntityMgr.GetFromPool("Enemy1");
            entity.transform.position = transform.position;
            Logger.Instance.Log($"Enemy1 try instantiate: {entity}");
        });
    }
}
