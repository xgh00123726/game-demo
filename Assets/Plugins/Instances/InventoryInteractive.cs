using GameBase.EntitySystem;
using GameBase.Tools;
using System;

namespace Instance
{
    public class InventoryInteractive : SingletonInstance<InventoryInteractive>
    {
        public static Func<bool> ActiveCmd;
        public static Func<bool> InActiveCmd;
        public static Action OnActive;
        public static Action OnInActive;

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
