using UnityEngine;

public class UIMediator : MonoBehaviour
{ 
    [SerializeField] private TickClock _tick;
    [SerializeField] private TimeViewer _timeViewer;
    [SerializeField] private TimeEditor timeEditor;
    [SerializeField] private HourHand _hourHand;
    [SerializeField] private MinuteHand _minuteHand;
    [SerializeField] private SecondHand _secondHand;

    private void OnEnable()
    {
        _tick.ChangeTime += _timeViewer.OnChangeTime;
        _tick.ChangeTime += timeEditor.OnChangeTime;
        _tick.ChangeTime += _hourHand.OnChangeTime;
        _tick.ChangeTime += _minuteHand.OnChangeTime;
        _tick.ChangeTime += _secondHand.OnChangeTime;
        
        timeEditor.TimeEdit += _tick.OnEditTime;
        
        _hourHand.ChangeTimeWithMouse += _tick.OnEditTime;
        _minuteHand.ChangeTimeWithMouse += _tick.OnEditTime;
    }

    private void OnDisable()
    {
        _tick.ChangeTime -= _timeViewer.OnChangeTime;
        _tick.ChangeTime -= timeEditor.OnChangeTime;
        _tick.ChangeTime -= _hourHand.OnChangeTime;
        _tick.ChangeTime -= _minuteHand.OnChangeTime;
        _tick.ChangeTime -= _secondHand.OnChangeTime;
        
        timeEditor.TimeEdit -= _tick.OnEditTime;
        
        _hourHand.ChangeTimeWithMouse -= _tick.OnEditTime;
        _minuteHand.ChangeTimeWithMouse -= _tick.OnEditTime;
    }
}