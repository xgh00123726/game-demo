using GameBase.Infos;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public class CommonInventoryViewPanel : BaseViewPanel<CommonInventoryViewItem>
    {
        protected float initX;
        protected float initY;

        protected float panelXOffset = 0f;
        protected float panelYOffset = 0f;

        public float panelXMoveSpeed = 100f;
        public float panelYMoveSpeed = 100f;
        public float panelXOffsetTarget = 0f;
        public float panelYOffsetTarget = 0f;

        private static CommonInventoryViewPanel _instance = new CommonInventoryViewPanel();
        public static CommonInventoryViewPanel Instance => _instance;

        public CommonInventoryViewPanel(int prefabID = 35,
            int defaultObjID = 34) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("error");
            }
        }

        protected override void Update()
        {
            base.Update();

            var delta = panelXOffset - panelXOffsetTarget;
            var xMoveDis = panelXMoveSpeed * Time.deltaTime;
            if (delta > xMoveDis)
            {
                panelXOffset -= xMoveDis;
            }
            else if (-delta > xMoveDis)
            {
                panelXOffset += xMoveDis;
            }
            else
            {
                panelXOffset = panelXOffsetTarget;
            }

            panel.transform.localPosition = new Vector3(initX + panelXOffset, initY + panelYOffset, 0f);
        }

        public override void SetLocalPosition(float x, float y)
        {
            panel.transform.localPosition = new Vector3(x, y, 0);
            initX = x;
            initY = y;
        }
    }
}
