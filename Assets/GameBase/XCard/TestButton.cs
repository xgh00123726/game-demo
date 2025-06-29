using System.Collections;
using System.Collections.Generic;
using GameBase.Tools;
using GameBase.Resources;
using GameBase.XCard;
using UnityEngine;
using UnityEngine.Assertions;

public class TestButton : MonoBehaviour
{
    public InteractiveComponent interactiveComponent;
    public int cardNum = 10;
    public float leftEdge = 0;
    public float rightEdge = 10;
    public float y = 0;
    public float disInterval = 0.5f;
    public float getInterval = 0.2f;
    
    private bool _getFlag = true;
    private bool _getOver = true;
    private List<XCardBase> _cards = new List<XCardBase>();
    private void Awake()
    {
        interactiveComponent = GetComponent<InteractiveComponent>();
        Assert.IsNotNull(interactiveComponent);

        interactiveComponent.OnMouseUpAsButtonAction = GetOrLoseCardsInHand;
    }
    private void GetOrLoseCardsInHand()
    {
        if (!_getOver) return;

        if (_getFlag)
        {
            _getOver = false;
            Timer.AddTask(getInterval, cardNum, true, () =>
            {
                var card = PrefabMgr.GetFromPool<XCardBase>(PrefabType.Entity);
                Vector3 pos = Vector3.zero;
                pos.x = (rightEdge + leftEdge) / 2 + (_cards.Count - (cardNum - 1) / 2) * disInterval;
                pos.y = y;
                pos.z = card.transform.position.z;
                card.transform.position = pos;
                _cards.Add(card);
                if (_cards.Count == cardNum) _getOver = true;
            });
        }
        else
        {
            foreach (var card in _cards)
            {
                PrefabMgr.TryReleaseToPool(card);
            }
            _cards.Clear();
        }

        _getFlag = !_getFlag;
    }

    private void Update()
    {
        
    }
}
