using UnityEngine;

namespace GameBase.UI
{
    public abstract class DetailableBaseItem : BasePanelItem, 
        IDetailable
    {
        public DetailContent detailContent = new();

        public virtual int DetailUITexureID { get; } = 0;

        DetailContent IDetailable.Content => detailContent;

        bool IDetailable.IsPointerOn => Obj.isPointerOn;

        public virtual Vector3 ShowPosition => Obj.transform.position;

        public virtual void OnPointerEnter() { }
        public virtual void OnPointerExist() { }
    }
}
