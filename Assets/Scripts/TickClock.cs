using System;
using System.Collections;
using Unity.VisualScripting;
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
    private void Start()
    {
        StartCoroutine(Tick());
    }

    public void OnEditTime(DateTime time)
    {
        _currentTime = time;
        ChangeTime?.Invoke(_currentTime);
    }
    

    private IEnumerator Tick()
    {
        yield return StartCoroutine(_timeService.LoadTimeFromServer());
        
        ParseResponse(_timeService.Result);
        // while (!_timeService.Success)
        // {
        //     ParseResponse(_timeService.Result);
        // }
        
        while (true)
        {
            _currentTime = _currentTime.AddSeconds(GlobalConstants.ConstantsForTime.TICK);
            
            ChangeTime?.Invoke(_currentTime);
            
            yield return new WaitForSeconds(GlobalConstants.ConstantsForTime.TICK);
        }
    }

    private void ParseResponse(string response)
    {
        var json = JsonUtility.FromJson<TimeTemplate>(response);
        var offset = DateTimeOffset.FromUnixTimeMilliseconds(json.time).ToLocalTime();

        _currentTime = offset.DateTime;
    }

}
