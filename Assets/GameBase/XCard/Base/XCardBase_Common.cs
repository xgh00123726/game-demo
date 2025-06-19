using System.Collections;
using System.Collections.Generic;
using GameBase.GCamera;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.XCard
{
    public partial class XCardBase : PoolablePrefab
    {
        InteractiveComponent interactiveComponent;
        Transform body;
        Material bodyMaterial;
        Material suitMaterialTop;
        Material suitMaterialBottom;
        XCDescribe describe;
        private SuitType _suit;
        public SuitType Suit
        {
            get => _suit;
            set
            {
                if (_suit != value)
                {
                    _suit = value;
                    SetSuit(value);
                }
            }
        }

        #region CardDrag
        /*
         * 实现类似炉石传说和杀戮尖塔的卡牌拖拽功能
         * 1 左键点击卡牌且不松开
         *  1.1 当卡牌在手中时，不送开时卡牌随鼠标移动
         *  1.2 当卡牌在手中时，松开后卡牌就会一直跟着鼠标了
         *  1.3 当卡牌不在手中时，不松开时卡牌随鼠标移动
         *  1.4 当卡牌不在手中时，松开后卡牌被使用
         * 2 左键点击卡牌后立马松开
         *  2.1 如果鼠标左键不再次按下，卡牌一直随鼠标移动
         *  2.2 再次左键点击不松开，什么事都不发生
         *  2.3 再次点击左键且松开，当卡牌在手中时，卡牌归位
         *  2.4 再次点击左键且松开，当卡牌不在手中时，使用卡牌
         */
        bool _isInHand => HandArea.IsMouseOn;
        bool _isFloat = false;
        #endregion

        protected virtual void OnSpell()
        {
            Timer.AddTask(1f, () =>
            {
                PrefabMgr.Instance.ReleaseToPool(this);
            });
            AnimateLoss();
        }

        protected virtual void OnPointerDown()
        {
            if (!_isFloat)
            {
                _isFloat = true;
            }
        }
        protected virtual void OnPointerUp()
        {
        }

        protected virtual void OnMouseUpAsButton()
        {
            if (_isFloat)
            {
                _isFloat = false;
                if (!_isInHand)
                {
                    OnSpell();
                }
            }
        }
        protected virtual void OnPointerEnter()
        {
            bodyMaterial.color = Color.red;
            CardDescriber.Active = true;
            CardDescriber.Text = describe.ToString();
            CardDescriber.Position = UnityEngine.Input.mousePosition;
        }

        protected virtual void OnPointerExit()
        {
            bodyMaterial.color = Color.white;
            CardDescriber.Active = false;
        }

        protected override void OnInstantiate()
        {
            gameObject.SetActive(true);
            bodyMaterial.color = Color.white;
            bodyMaterial.SetFloat("_Loss", 0f);
        }

        private void CommonInit()
        {
            interactiveComponent = GetComponentInChildren<InteractiveComponent>();
            Assert.IsNotNull(interactiveComponent);

            interactiveComponent.OnMouseUpAction = OnPointerUp;
            interactiveComponent.OnMouseDownAction = OnPointerDown;
            interactiveComponent.OnMouseEnterAction = OnPointerEnter;
            interactiveComponent.OnMouseExitAction = OnPointerExit;
            interactiveComponent.OnMouseUpAsButtonAction = OnMouseUpAsButton;

            body = transform.Find("Body");
            Assert.IsNotNull(body);

            bodyMaterial = body.GetComponent<MeshRenderer>().material;
            Assert.IsNotNull(bodyMaterial);

            suitMaterialTop = transform.Find("Suit_Top").GetComponent<MeshRenderer>().material;
            suitMaterialBottom = transform.Find("Suit_Bottom").GetComponent<MeshRenderer>().material;
            Assert.IsNotNull(suitMaterialTop);
            Assert.IsNotNull(suitMaterialBottom);


            describe = new XCDescribe("<color=\"green\"><keyword-invoke></keyword-invoke></color><content-1>");
        }
    }
}
