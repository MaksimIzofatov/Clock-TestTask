using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMediator : MonoBehaviour
{
    [SerializeField] private HandsRotator _rotator;
    [SerializeField] private TimeViewer _timeViewer;

    private void OnEnable()
    {
        _rotator.ChangeTime += _timeViewer.OnChangeTime;
    }

    private void OnDisable()
    {
        _rotator.ChangeTime -= _timeViewer.OnChangeTime;
    }
}
