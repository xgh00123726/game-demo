using GameBase.EntitySystem;
using GameBase.Tools;
namespace GameBase.Modify
{
    public class ModifyableSys : CommonEntitySys<Modifyable, ModifyableSys>
    {
        protected override void OnRegisterEntityToActives(Modifyable e)
        {
        }

        protected override void OnRemoveEntityFromActives(Modifyable e)
        {
            e.modifyers.Clear();
        }


        protected override void UpdateEntity(Modifyable e)
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


            float finnalVal = e.valueSet;
            foreach (var modifyer in e.modifyers)
            {
                if (modifyer.isRelease) // modifyer触发自身的移除条件后，也要从modifyerable的列表中移除
                {
                    e.RemoveModify(modifyer);
                    continue;
                }

                if (!modifyer.enable) continue;  // modifyer不使能则不生效

                if ((modifyer.type & ModifyType.Temporary) != 0)
                {
                    finnalVal += modifyer.value;
                }
                else if ((modifyer.type & ModifyType.Forever) != 0)
                {
                    e.valueSet += modifyer.value;
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

        internal void InternalRemoveEntity(Modifyable e)
        {
            RemoveEntity(e);
        }

        public Modifyable NewEntity(float initValue)
        {
            var ret = Instance.NewEntity();
            ret.valueSet = initValue;
            ret.value = initValue;

            return ret;
        }
    }
}
