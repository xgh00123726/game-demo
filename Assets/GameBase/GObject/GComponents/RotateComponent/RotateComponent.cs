using System;
using System.Collections;
using System.Collections.Generic;
using GameBase.Object;
using GameBase.Math;
using UnityEngine;

public class RotateComponent : GComponent
{
    public float _eulerXSet = 0f;
    public float _eulerYSet = 0f;
    public float _eulerZSet = 0f;

    public float _eulerX = 0f;
    public float _eulerY = 0f;
    public float _eulerZ = 0f;

    public float _eulerXTarget = 0f;
    public float _eulerYTarget = 0f;
    public float _eulerZTarget = 0f;

    private bool _lastInRotating = false;
    private bool _inRotating = false;
    public bool InRotating => _inRotating;

    public Vector3 rotateSpeed = Vector3.one;  // 每秒钟可以旋转的角度
    public Vector3 rotateDirction = Vector3.one;


    public delegate void RotateEndCallback();
    private RotateEndCallback _OnRotateEnd;

    public void ResetEuler(bool immediately = false)
    {
        if (immediately)
        {
            _eulerX = _eulerXSet;
            _eulerY = _eulerYSet;
            _eulerZ = _eulerZSet;
            _eulerXTarget = _eulerXSet;
            _eulerYTarget = _eulerYSet;
            _eulerZTarget = _eulerZSet;
        }
        else
        {
            _eulerXTarget = _eulerXSet;
            _eulerYTarget = _eulerYSet;
            _eulerZTarget = _eulerZSet;
        }
    }

    public void RotateX(float angle)
    {
        _eulerXTarget = _eulerX + angle;
        rotateDirction.Set(Mathf.Sign(angle), rotateDirction.y, rotateDirction.z);
    }
    public void RotateY(float angle)
    {
        _eulerYTarget = _eulerY + angle;
        rotateDirction.Set(rotateDirction.x, Mathf.Sign(angle), rotateDirction.z);
    }
    public void RotateZ(float angle)
    {
        _eulerZTarget = _eulerZ + angle;
        rotateDirction.Set(rotateDirction.x, rotateDirction.y, Mathf.Sign(angle));
    }
    public void Stop()
    {
        _eulerXTarget = _eulerX;
        _eulerYTarget = _eulerY;
        _eulerZTarget = _eulerZ;
    }

    /* @FUNC : 旋转物体直到前朝向(transform.forward)与targetDir重合
     */
    public void RotateForward(Vector3 targetDir)
    {
        float rotateAngle = GMath.AngleOfLines2D(transform.forward, targetDir, GMath.Axis.Y) * Mathf.Rad2Deg;
        float cross = transform.forward.x * targetDir.z - transform.forward.z * targetDir.x;
        if (cross < 0)
        {
            RotateY(rotateAngle);
        }
        else
        {
            RotateY(-rotateAngle);
        }
    }

    /* @FUNC : 旋转物体直到前朝向(transform.forward)与targetDir重合，旋转结束时调用OnRotateEnd
     * @COMM : 如果
    */
    public void RotateForward(Vector3 targetDir, RotateEndCallback OnRotateEnd)
    {
        RotateForward(targetDir);
        _OnRotateEnd = OnRotateEnd;
    }

    protected void Awake()
    {
        ResetEuler(true);
    }

    protected void Update()
    {
        float xExpect = _eulerX + rotateSpeed.x * rotateDirction.x * Time.deltaTime;
        float yExpect = _eulerY + rotateSpeed.y * rotateDirction.y * Time.deltaTime;
        float zExpect = _eulerZ + rotateSpeed.z * rotateDirction.z * Time.deltaTime;

        bool xBeyond =
            (xExpect > _eulerXTarget && rotateDirction.x > 0) ||
            (xExpect < _eulerXTarget && rotateDirction.x < 0) ||
            rotateDirction.x == 0;
        bool yBeyond =
            (yExpect > _eulerYTarget && rotateDirction.y > 0) ||
            (yExpect < _eulerYTarget && rotateDirction.y < 0) ||
            rotateDirction.y == 0;
        bool zBeyond =
            (zExpect > _eulerZTarget && rotateDirction.z > 0) ||
            (zExpect < _eulerZTarget && rotateDirction.z < 0) ||
            rotateDirction.z == 0;

        _eulerX = xBeyond ? _eulerXTarget : xExpect;
        _eulerY = yBeyond ? _eulerYTarget : yExpect;
        _eulerZ = zBeyond ? _eulerZTarget : zExpect;

        _inRotating = !(xBeyond && yBeyond && zBeyond);

        if (_lastInRotating && !_inRotating)
        {
            _OnRotateEnd?.Invoke();
            _OnRotateEnd = null;
        }
        if (_inRotating && !_lastInRotating)
        {

        }

        _lastInRotating = _inRotating;

        transform.rotation = Quaternion.Euler(_eulerX, _eulerY, _eulerZ);
    }
}
