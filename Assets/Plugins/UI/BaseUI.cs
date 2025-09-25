using GameBase.Tools;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class BaseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        protected internal float enterTime;
        protected internal float pointerDownTime;
        protected internal bool isPointerOn;
        protected internal bool isPointerDown;
        protected internal int index;
        public IEnterExitControl enterExitControl;

        public float EnterTime => enterTime;
        public float PointerDownTime => pointerDownTime;
        public bool IsPointerOn => isPointerOn;
        public bool IsPointerDown => isPointerDown;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (isPointerOn)
                {
                    isPointerDown = true;
                }
                enterExitControl?.OnPointerDown(index);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                enterExitControl?.OnPointerRightDown(index);
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            isPointerOn = true;
            enterExitControl?.OnPointerEnter(index);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            enterTime = 0;
            isPointerOn = false;
            enterExitControl?.OnPointerExit(index);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            pointerDownTime = 0;
        }
    }
}
