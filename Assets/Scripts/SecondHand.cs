using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SecondHand : HandBase
{
    protected  override void Rotate()
    {
        float secondAngle = _currentTime.Second * GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND;
        
        transform.DOLocalRotate(Quaternion.Euler(0, 0, -secondAngle).eulerAngles, 
            GlobalConstants.ConstantsForTime.TICK);
    }
}
