using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class ShopViewPanel : BaseViewPanel<ShopViewItem>
    {
        private BaseUI _refreshIconScript;

        private static ShopViewPanel _instance = new ShopViewPanel(41, 40);
        public static ShopViewPanel Instance => _instance;

        public ShopViewPanel(int prefabID = 41, int defaultObjID = 40) : base(prefabID, defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _refreshIconScript = panel.transform.Find("RefreshIcon").gameObject.AddComponent<BaseUI>();
        }

        protected override BaseUI InstantiateObj(ShopViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.identifyIconObj = e.obj.transform.Find("Identify").gameObject;

            var image = e.identifyIconObj.GetComponent<Image>();
            e.identifyImage = new SuperImage(image);

            e.priceText = e.obj.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        public void SetRefreshIconController(IEnterExitControl controller)
        {
            _refreshIconScript.enterExitControl = controller;
        }
    }
}
