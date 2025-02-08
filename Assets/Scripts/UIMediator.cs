using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIMediator : MonoBehaviour
{ 
    [SerializeField] private TickClock _tick;
    [SerializeField] private TimeViewer _timeViewer;
    [SerializeField] private TimeEditer _timeEditer;
    [SerializeField] private HourHand _hourHand;
    [SerializeField] private MinuteHand _minuteHand;
    [SerializeField] private SecondHand _secondHand;

    private void OnEnable()
    {
        _tick.ChangeTime += _timeViewer.OnChangeTime;
        _tick.ChangeTime += _timeEditer.OnChangeTime;
        _tick.ChangeTime += _hourHand.OnChangeTime;
        _tick.ChangeTime += _minuteHand.OnChangeTime;
        _tick.ChangeTime += _secondHand.OnChangeTime;
        
        _timeEditer.TimeEdit += _tick.OnEditTime;
        
        _hourHand.MouseMove += _tick.OnEditTime;
        _minuteHand.MouseMove += _tick.OnEditTime;
    }

    private void OnDisable()
    {
        _tick.ChangeTime -= _timeViewer.OnChangeTime;
        _tick.ChangeTime -= _timeEditer.OnChangeTime;
        _tick.ChangeTime -= _hourHand.OnChangeTime;
        _tick.ChangeTime -= _minuteHand.OnChangeTime;
        _tick.ChangeTime -= _secondHand.OnChangeTime;
        
        _timeEditer.TimeEdit -= _tick.OnEditTime;
        
        _hourHand.MouseMove -= _tick.OnEditTime;
        _minuteHand.MouseMove -= _tick.OnEditTime;
    }
}
