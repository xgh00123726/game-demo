using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Tools;

namespace GameBase.Equipments
{
    public class EquipmentSys : SealedEntitySys<Equipment, EquipmentSys>
    {
        protected override void OnGet(Equipment e)
        {
            e.TextureName = null;
        }

        protected override void OnRelease(Equipment e)
        {
            foreach (var m in e.Modifyers)
            {
                m.Release();
            }
            e.Modifyers.Clear();
            e.Owner.OnRemoveEquipment(e);
        }

        protected override void EntityStart(Equipment e)
        {
            if (e.Owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("equipment has no owner");
                RemoveEntity(e);
                return;
            }

            foreach (var kvp in e.IntKeyModifiers)
            {
                var m = ModifyerSys.Instance.NewEntity();
                m.Value = kvp.Value;
                m.Type = ModifyType.Temporary | ModifyType.Always;
                m.AddTo(e.Owner.Modifyables[kvp.Key]);
                e.Modifyers.Add(m);
            }

            e.Owner.OnGetEquipment(e);
        }
    }
}
