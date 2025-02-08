using DG.Tweening;
using UnityEngine;

public class HourHand : HandBase
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
        int hours = (int)(-angle / GlobalConstants.ConstantsForTime.GRADUS_FOR_HOUR);

        _editedTime = _currentTime.AddHours(hours - _currentTime.Hour);
    }

    protected override void Rotate()
    {
        if (!_isEdit)
        {
            float hourAngle = (_currentTime.Hour % GlobalConstants.ConstantsForTime.COUNT_HOURS
                               + _currentTime.Minute / GlobalConstants.ConstantsForTime.COUNT_MINUTES_AND_SECONDS) 
                              * GlobalConstants.ConstantsForTime.GRADUS_FOR_HOUR; 

            _transform.DOLocalRotate(Quaternion.Euler(0, 0, -hourAngle).eulerAngles, 
                GlobalConstants.ConstantsForTime.TICK);
        }
    }
}
