using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SecondHand : MonoBehaviour
{
    private DateTime _currentTime;
    public void OnChangeTime(DateTime time)
    {
        _currentTime = time;
        Rotate();
    }
    private void Rotate()
    {
        float secondAngle = _currentTime.Second * GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND;
        
        transform.DOLocalRotate(Quaternion.Euler(0, 0, -secondAngle).eulerAngles, 
            GlobalConstants.ConstantsForTime.TICK);
    }
}
