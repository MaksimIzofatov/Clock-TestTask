using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HandsRotator : MonoBehaviour
{
    public Action<DateTime> ChangeTime;
    
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

        _hourHand.transform.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        _minuteHand.transform.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        _secondHand.transform.localRotation = Quaternion.Euler(0, 0, -secondAngle);
    }
}
