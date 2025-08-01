using System;
using GameBase.Object;
using UnityEngine;
using UnityEngine.Assertions;

public class InteractiveComponent : GComponent
{
    public bool _isMouseStay;
    private float _mouseEnterTime = 0f;
    public float MouseStayTime => _isMouseStay ? Time.time - _mouseEnterTime : 0f;
    public bool IsMouseStay => _isMouseStay;
    public Action OnMouseDownAction { set; private get; }
    public Action OnMouseEnterAction { set; private get; }
    public Action OnMouseExitAction { set; private get; }
    public Action OnMouseUpAction { set; private get; }
    public Action OnMouseUpAsButtonAction { set; private get; }
    public Action OnMouseDragAction { set; private get; }
    public Action OnMouseOverAction { set; private get; }

    private void Awake()
    {
        Assert.IsTrue(GetComponent<BoxCollider2D>() != null ||
            GetComponent<CircleCollider2D>() != null ||
            GetComponent<CapsuleCollider2D>() != null);
    }

    private void OnMouseDown()
    {
        OnMouseDownAction?.Invoke();
    }

    private void OnMouseEnter()
    {
        OnMouseEnterAction?.Invoke();
        _isMouseStay = true;
        _mouseEnterTime = Time.time;
    }

    private void OnMouseExit()
    {
        OnMouseExitAction?.Invoke();
        _isMouseStay = false;
    }

    private void OnMouseUp()
    {
        OnMouseUpAction?.Invoke();
    }

    private void OnMouseUpAsButton()
    {
        OnMouseUpAsButtonAction?.Invoke();
    }

    private void OnMouseDrag()
    {
        OnMouseDragAction?.Invoke();
    }

    private void OnMouseOver()
    {
        OnMouseOverAction?.Invoke();
    }
}
