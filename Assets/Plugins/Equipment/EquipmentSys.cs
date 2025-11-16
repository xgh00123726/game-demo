using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Tools;

namespace GameBase.Equipments
{
    public class EquipmentSys : SealedEntitySys<Equipment, EquipmentSys>
    {
        protected override void OnGet(Equipment e)
        {
            e.textureName = null;
        }

        protected override void OnRelease(Equipment e)
        {
            foreach (var m in e.modifyers)
            {
                m.Release();
            }
            e.modifyers.Clear();
            e.owner.OnRemoveEquipment(e);
        }

        protected override void EntityStart(Equipment e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("equipment has no owner");
                RemoveEntity(e);
                return;
            }

            foreach (var kvp in e.iModifiers)
            {
                var m = ModifyerSys.Instance.NewEntity();
                m.value = kvp.Value;
                m.type = ModifyType.Temporary | ModifyType.Always;
                m.AddTo(e.owner.Modifyables[kvp.Key]);
                e.modifyers.Add(m);
            }

            e.owner.OnGetEquipment(e);
        }
    }
}
