using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class BaseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {        
        internal float enterTime;
        internal float pointerDownTime;
        internal bool isPointerOn;
        internal bool isPointerDown;
        internal Action enterAction;
        internal Action exitAction;

        public float EnterTime => enterTime;
        public float PointerDownTime => pointerDownTime;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (isPointerOn)
            {
                isPointerDown = true;
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            isPointerOn = true;
            enterAction?.Invoke();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            enterTime = 0;
            isPointerOn = false;
            exitAction?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            pointerDownTime = 0;
        }
    }
}
