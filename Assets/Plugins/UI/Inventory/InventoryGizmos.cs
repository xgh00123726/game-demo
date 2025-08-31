using GameBase.Math;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.UI
{
    public class InventoryGizmos : MonoBehaviour
    {
        public bool isInit = false;
        public bool drawGizmos = false;
        public static InventoryPanel sysInstance;
        public static InventoryGizmos Instance => _instance;
        public static InventoryGizmos _instance;

        public void Init(InventoryPanel panel)
        {
            sysInstance = panel;
        }

        public void ToggleShow()
        {
            drawGizmos = !drawGizmos;
        }

        private void Awake()
        {
            _instance = this;
            Command.Register("toggle-inventory-gizmos", ToggleShow);
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
            {
                return;
            }
            foreach (var e in sysInstance.Entities)
            {
                Gizmos.color = Color.green;
                Rect rl = e.Obj.transform.Find("Icon").GetComponent<RectTransform>().rect;
                rl.center = e.Obj.transform.position;
                GMath.Rect2D rg = new GMath.Rect2D(rl);
                GizmosAppend.DrawRect(rg);
            }
        }
    }
}
