using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class MinuteHand : HandBase
{
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _camera = Camera.main;
    }

    private void OnMouseDrag()
    {
       EditTime();
    }

    protected override void SetTime(float angle)
    {
        int minutes = (int)(-angle / GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND);

        _editedTime = _currentTime.AddMinutes(minutes - _currentTime.Minute);
    }

    protected override void Rotate()
    {
        if (!_isEdit)
        {
            float minuteAngle = (_currentTime.Minute +
                                 _currentTime.Second / GlobalConstants.ConstantsForTime.COUNT_MINUTES_AND_SECONDS)
                                * GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND;

            _transform.DOLocalRotate(Quaternion.Euler(0, 0, -minuteAngle).eulerAngles,
                GlobalConstants.ConstantsForTime.TICK);
        }
    }
}
