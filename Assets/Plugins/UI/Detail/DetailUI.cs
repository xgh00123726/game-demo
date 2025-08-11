using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class DetailUI : BasePanelItem
    {
        public DetailUI()
        {
            ObjID = 4;
        }

        internal override int IconTexureID => detailables[0].TexureID;

        public List<IDetailable> detailables = new List<IDetailable>();
        
        internal TextMeshProUGUI textTMP;
    }
}
