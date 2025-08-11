using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class BaseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static Func<bool> SwitchTrigger;

        internal bool isPointerOn;
        internal Action OnPointerEnter;
        internal Action OnPointerExit;
        internal Action OnSwitchOn;
        internal Action OnSwitchOff;

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            isPointerOn = true;
            OnPointerEnter?.Invoke();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            isPointerOn = false;
            OnPointerExit?.Invoke();
        }
    }
}
