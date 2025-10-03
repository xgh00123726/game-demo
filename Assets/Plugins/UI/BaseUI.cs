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

        public Action<int> OnPointerDown;
        public Action<int> OnPointerRightDown;
        public Action<int> OnPointerUp;
        public Action<int> OnPointerEnter;
        public Action<int> OnPointerExit;

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
                OnPointerDown?.Invoke(index);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnPointerRightDown?.Invoke(index);
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            isPointerOn = true;
            OnPointerEnter?.Invoke(index);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            enterTime = 0;
            isPointerOn = false;
            OnPointerExit?.Invoke(index);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            pointerDownTime = 0;
            OnPointerUp?.Invoke(index);
        }
    }
}
