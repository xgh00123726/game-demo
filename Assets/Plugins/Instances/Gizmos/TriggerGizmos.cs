using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Instance
{
    public class TriggerGizmos : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            if (!GizmosCfg.isDrawGizmos)
            {
                return;
            }
            foreach (var e in TriggerSys.Instance.Entities)
            {
                Gizmos.color = Color.green;
                GizmosAppend.DrawShape(e.shape, GizmosCfg.drawY);
            }
        }
    }
}
