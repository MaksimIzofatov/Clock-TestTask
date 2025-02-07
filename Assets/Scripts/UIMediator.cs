using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMediator : MonoBehaviour
{
    [SerializeField] private HandsRotator _rotator;
    [SerializeField] private TimeViewer _timeViewer;
    [SerializeField] private TimeEditer _timeEditer;
    [SerializeField] private InputMouse _inputMouse;

    private void OnEnable()
    {
        _rotator.ChangeTime += _timeViewer.OnChangeTime;
        _rotator.ChangeTime += _timeEditer.OnChangeTime;
        _timeEditer.TimeEdit += _timeViewer.OnChangeTime;
        _timeEditer.TimeEdit += _rotator.OnEditTime;
        _inputMouse.MouseMove += _rotator.OnMouseMove;
    }

    private void OnDisable()
    {
        _rotator.ChangeTime -= _timeViewer.OnChangeTime;
        _rotator.ChangeTime -= _timeEditer.OnChangeTime;
        _timeEditer.TimeEdit -= _timeViewer.OnChangeTime;
        _timeEditer.TimeEdit -= _rotator.OnEditTime;
        _inputMouse.MouseMove -= _rotator.OnMouseMove;
    }
}
