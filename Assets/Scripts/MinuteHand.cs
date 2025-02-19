using DG.Tweening;
using UnityEngine;

public class MinuteHand : HandBase
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
        int minutes = (int)(-angle / GRADUS_FOR_MINUTE_AND_SECOND);

        _editedTime = _currentTime.AddMinutes(minutes - _currentTime.Minute);
    }

    protected override void Rotate()
    {
        if (!_isEdit)
        {
            float minuteAngle = (_currentTime.Minute +
                                 _currentTime.Second / COUNT_MINUTES_AND_SECONDS)
                                * GRADUS_FOR_MINUTE_AND_SECOND;

            _transform.DOLocalRotate(Quaternion.Euler(0, 0, -minuteAngle).eulerAngles,
                TICK);
        }
    }
}