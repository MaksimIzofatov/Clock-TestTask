using DG.Tweening;
using UnityEngine;

public class HourHand : HandBase
{
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnMouseDrag()
    {
        EditTime();
    }

    protected override void SetTime(float angle)
    {
        int hours = (int)(-angle / GRADUS_FOR_HOUR);

        _editedTime = _currentTime.AddHours(hours - _currentTime.Hour);
    }

    protected override void Rotate()
    {
        if (!_isEdit)
        {
            float hourAngle = (_currentTime.Hour 
                               + _currentTime.Minute / COUNT_MINUTES_AND_SECONDS) 
                              * GRADUS_FOR_HOUR; 

            _transform.DOLocalRotate(Quaternion.Euler(0, 0, -hourAngle).eulerAngles, 
                TICK);
        }
    }
}