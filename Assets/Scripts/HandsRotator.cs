using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class HandsRotator : MonoBehaviour
{
    public event Action<DateTime> ChangeTime;
    
    [SerializeField] private GameObject _hourHand;
    [SerializeField] private GameObject _minuteHand;
    [SerializeField] private GameObject _secondHand;
    
    private TimeService _timeService;
    private DateTime _currentTime;

    [Inject]
    private void Constructor(TimeService time)
    {
        _timeService = time;
    }
    private async void Start()
    {
        _currentTime = await _timeService.LoadTimeFromServer();
        
        RotateHands();
        StartCoroutine(Tick());
    }

    public void OnEditTime(DateTime time)
    {
        _currentTime = time;
    }
    public void OnMouseMove(bool isHourHand, Vector3 position)
    {
        var vectorHand = isHourHand ? _hourHand.transform.position : _minuteHand.transform.position;
        var direction = vectorHand - position;
        var angle = Vector3.Angle(direction,
            position);
        
        RotateHands(isHourHand, angle);
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            _currentTime = _currentTime.AddSeconds(GlobalConstants.ConstantsForTime.TICK);
            
            RotateHands();
            ChangeTime?.Invoke(_currentTime);
            
            yield return new WaitForSeconds(GlobalConstants.ConstantsForTime.TICK);
        }
    }

    private void RotateHands()
    {
        float hourAngle = (_currentTime.Hour % GlobalConstants.ConstantsForTime.COUNT_HOURS
                           + _currentTime.Minute / GlobalConstants.ConstantsForTime.COUNT_MINUTES_AND_SECONDS) 
                           * GlobalConstants.ConstantsForTime.GRADUS_FOR_HOUR; 
        float minuteAngle = (_currentTime.Minute + _currentTime.Second / GlobalConstants.ConstantsForTime.COUNT_MINUTES_AND_SECONDS) 
                            * GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND; 
        float secondAngle = _currentTime.Second * GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND;

        _hourHand.transform.DOLocalRotate(Quaternion.Euler(0, 0, -hourAngle).eulerAngles, 
                                            GlobalConstants.ConstantsForTime.TICK);
        _minuteHand.transform.DOLocalRotate(Quaternion.Euler(0, 0, -minuteAngle).eulerAngles,
                                            GlobalConstants.ConstantsForTime.TICK);
        _secondHand.transform.DOLocalRotate(Quaternion.Euler(0, 0, -secondAngle).eulerAngles, 
                                            GlobalConstants.ConstantsForTime.TICK);
    }

    private void RotateHands(bool isHourHand, float angle)
    {
        if (isHourHand)
            _hourHand.transform.DOLocalRotate(Quaternion.Euler(0, 0, -angle).eulerAngles, 
                GlobalConstants.ConstantsForTime.TICK);
        else
            _minuteHand.transform.DOLocalRotate(Quaternion.Euler(0, 0, -angle).eulerAngles,
            GlobalConstants.ConstantsForTime.TICK);
    }
}
