using GameBase.EntitySystem;
using GameBase.Tools;
namespace GameBase.Modify
{
    public class ModifyableSys<T> : SimplestEntitySys<Modifyable<T>, SimpleEntityContainer, ModifyableSys<T>>
    {
        protected override void OnRegisterEntityToActives(Modifyable<T> e)
        {
            foreach (var func in e.RegistertoActivesDelegate)
            {
                func?.Invoke(e);
            }
        }

        protected override void OnRemoveEntityFromActives(Modifyable<T> e)
        {
            foreach (var func in e.RemoveFromActiveDelegate)
            {
                func?.Invoke(e);
            }
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
                if (modifyer.isRelease) // modifyer触发自身的移除条件后，也要从modifyerable的列表中移除
                {
                    e.RemoveModify(modifyer);
                    continue;
                }

                if (!modifyer.enable) continue;  // modifyer不使能则不生效

                if (modifyer.ModifyFunc == null) continue;  // 没有modifyerfunc也不生效

                if ((modifyer.type & ModifyType.Temporary) != 0)
                {
                    finnalVal = modifyer.ModifyFunc(finnalVal, e.valueSet);
                }
                else if ((modifyer.type & ModifyType.Forever) != 0)
                {
                    e.valueSet = modifyer.ModifyFunc(finnalVal, e.valueSet);
                }

                modifyer.OnModify?.Invoke();
                modifyer.enable = false;

                if ((modifyer.type & ModifyType.Once) != 0)
                {
                    modifyer.modifyableRelease = true;
                }
            }

            e.value = finnalVal;
        }

        public T_Return NewEntity<T_Return>(T initValue) where T_Return : Modifyable<T>, new()
        {
            var ret = Instance.NewEntity<T_Return>();
            ret.valueSet = initValue;
            ret.value = initValue;

            return ret;
        }
    }
}
