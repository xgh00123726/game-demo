using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class InventoryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public bool isPointerOn;
        public bool isPointerDown;
        public float pointerDownTime;
        public IEnterExist enterExist;

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            isPointerOn = true;
            enterExist?.OnPointerEnter();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            isPointerOn = false;
            enterExist?.OnPointerExist();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (isPointerOn)
            {
                isPointerDown = true;
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
        }
    }
}
