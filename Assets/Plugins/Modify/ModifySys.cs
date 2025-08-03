using GameBase.Tools;
namespace GameBase.Modify
{
    public class ModifySys<T> : SimplestEntitySys<Modifyable<T>, CSObjectPool<Modifyable<T>>, ModifySys<T>>
    {
        protected override void OnRegisterEntity(Modifyable<T> e)
        {
            
        }

        protected override void OnRemoveEntity(Modifyable<T> e)
        {
            
        }

        protected override void UpdateEntity(Modifyable<T> e)
        {
            T finnalVal = e.valueSet;
            foreach (var func in e.modifyBehaviors)
            {
                if (func != null)
                {
                    finnalVal = func(finnalVal);
                }
            }

            e.value = finnalVal;
        }
    }
}
