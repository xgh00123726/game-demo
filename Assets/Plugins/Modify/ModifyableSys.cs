using GameBase.EntitySystem;
using GameBase.Tools;
namespace GameBase.Modify
{
    public class ModifyableSys : SealedEntitySys<Modifyable, ModifyableSys>
    {
        public Modifyable NewEntity(float initValue)
        {
            var ret = Instance.NewEntity();
            ret.valueSet = initValue;
            ret.value = initValue;

            return ret;
        }

        protected override void UpdateEntity(Modifyable e)
        {
            
        }
    }
}
