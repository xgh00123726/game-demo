using GameBase.EntitySystem;
using GameBase.Tools;
using System;

namespace Instance
{
    public class InventoryInteractive : SingletonInstance<InventoryInteractive>
    {
        public static Func<bool> ActiveCmd { get; set; }
        public static Func<bool> InActiveCmd { get; set; }
        public static Action OnActive {  get; set; }
        public static Action OnInActive { get; set; }

        public InventoryInteractive()
        {
            ActiveCmd += DefaultActiveCmd;
            InActiveCmd += DefaultInActiveCmd;
        }

        private static bool DefaultActiveCmd()
        {
            var panel = InventoryViewPanel.Instance;
            if (panel == null)
            {
                return false;
            }

            if (!panel.IsShow && Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel, "inventory"))
            {
                return true;
            }

            return false;
        }

        private static bool DefaultInActiveCmd()
        {
            var panel = InventoryViewPanel.Instance;
            if (panel == null)
            {
                return false;
            }

            if (panel.IsShow && Inputs.GetKeyDown(KeyFunction.ToggleAttrPanel, "inventory"))
            {
                return true;
            }

            if (Inputs.GetKeyDown(KeyFunction.Cancel, "inventory"))
            {
                return true;
            }

            return false;
        }

        protected override void Update()
        {
            if (ActiveCmd?.Invoke() == true)
            {
                OnActive?.Invoke();
            }
            else if (InActiveCmd?.Invoke() == true)
            {
                OnInActive?.Invoke();
            }
        }
    }
}
