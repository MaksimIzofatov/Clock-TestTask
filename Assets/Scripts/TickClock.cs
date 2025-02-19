using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class TickClock : MonoBehaviour
{
    public event Action<DateTime> ChangeTime;

    [SerializeField] private GameObject _textError;
    
    private const int TICK_SECOND = 1;
    
    private TimeService _timeService;
    private DateTime _currentTime;
    private int _timeToErrorActive = 7;
    private int _tempTimeToErrorActive;

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
        
        if (_timeService.RequestIsSuccess)
        {
            ParseResponse(_timeService.Result);
        }
        else
        {
            _textError.SetActive(true);
            _currentTime = DateTime.Now;
        }
        
        while (true)
        {
            _currentTime = _currentTime.AddSeconds(TICK_SECOND);

            CountTimeForViewOfErrorMessage();
            
            ChangeTime?.Invoke(_currentTime);
            
            yield return new WaitForSeconds(TICK_SECOND);
        }
    }

    private void CountTimeForViewOfErrorMessage()
    {
        if (_tempTimeToErrorActive <= _timeToErrorActive)
            _tempTimeToErrorActive++;
        else
            _textError.SetActive(false);
    }

    private void ParseResponse(string response)
    {
        var json = JsonUtility.FromJson<TimeTemplate>(response);
        
        _currentTime = DateTime.Parse(json.dateTime);
    }
}