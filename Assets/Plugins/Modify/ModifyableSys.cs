using GameBase.Tools;
namespace GameBase.Modify
{
    public class ModifyableSys<T> : SimplestEntitySys<Modifyable<T>, SimpleEntityContainer, ModifyableSys<T>>
    {
        protected override void OnRegisterEntityToActives(Modifyable<T> e)
        {
            
        }

        protected override void OnRemoveEntityFromActives(Modifyable<T> e)
        {
            e.modifyers.Clear();
        }

        protected override void UpdateEntity(Modifyable<T> e)
        {
            foreach (var m in e.modifyersNeedAdd)
            {
                e.modifyers.AddLast(m);
            }
            e.modifyersNeedAdd.Clear();

            foreach (var m in e.modifyersNeedRemove)
            {
                e.modifyers.Remove(m);
            }
            e.modifyersNeedRemove.Clear();


            T finnalVal = e.valueSet;
            foreach (var modifyer in e.modifyers)
            {
                if (modifyer.isRelease)
                {
                    e.RemoveModify(modifyer);
                    continue;
                }

                if (!modifyer.enable) continue;

                if (modifyer.ModifyFunc == null) continue;

                finnalVal = modifyer.ModifyFunc(finnalVal, e.valueSet);
            }

            e.value = finnalVal;
        }
    }
}
