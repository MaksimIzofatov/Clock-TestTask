using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class TickClock : MonoBehaviour
{
    public event Action<DateTime> ChangeTime;
    
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
        
        StartCoroutine(Tick());
    }

    public void OnEditTime(DateTime time)
    {
        _currentTime = time;
        ChangeTime?.Invoke(_currentTime);
    }

    private IEnumerator Tick()
    {
        while (true)
        {
            _currentTime = _currentTime.AddSeconds(GlobalConstants.ConstantsForTime.TICK);
            
            ChangeTime?.Invoke(_currentTime);
            
            yield return new WaitForSeconds(GlobalConstants.ConstantsForTime.TICK);
        }
    }

}
