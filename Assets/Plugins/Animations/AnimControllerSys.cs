using GameBase.EntitySystem;
using GameBase.Tools;

namespace GameBase.Animations
{
    public class AnimControllerSys : InheritableSys<AnimController, AnimControllerSys>
    {
        protected override void UpdateEntity(AnimController e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("anim has no owner");
                RemoveEntity(e);
                return;
            }

            if (!e.owner.Alive)
            {
                RemoveEntity(e);
            }
            else
            {
                e.Update();
            }
        }
    }
}
