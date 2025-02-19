using DG.Tweening;
using UnityEngine;

public class SecondHand : HandBase
{
    protected  override void Rotate()
    {
        float secondAngle = _currentTime.Second * GRADUS_FOR_MINUTE_AND_SECOND;
        
        transform.DOLocalRotate(Quaternion.Euler(0, 0, -secondAngle).eulerAngles, 
            TICK);
    }
}