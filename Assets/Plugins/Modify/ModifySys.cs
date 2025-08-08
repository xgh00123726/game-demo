using GameBase.Tools;
namespace GameBase.Modify
{
    public class ModifySys<T> : SimplestEntitySys<Modifyable<T>, SimpleEntityContainer, ModifySys<T>>
    {
        protected override void OnRegisterEntityToActives(Modifyable<T> e)
        {
            
        }

        protected override void OnRemoveEntityFromActives(Modifyable<T> e)
        {
            e.modifyBehaviors.Clear();
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
