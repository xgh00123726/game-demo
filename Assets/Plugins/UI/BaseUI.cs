using GameBase.Tools;
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
        internal Action pointerDownAction;
        internal Action pointerRightDownAction;

        public float EnterTime => enterTime;
        public float PointerDownTime => pointerDownTime;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (isPointerOn)
                {
                    isPointerDown = true;
                }
                pointerDownAction?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                pointerRightDownAction?.Invoke();
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
