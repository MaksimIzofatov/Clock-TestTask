using System;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimeEditor : MonoBehaviour
{
    public event Action<DateTime> TimeEdit;
    
    [SerializeField] private GameObject _edit;
    [SerializeField] private GameObject _save;
    [SerializeField] private TMP_InputField _timeInput;
    
    private const string _timeFormat = "HH:mm:ss";
    
    private DateTime _currentTime;
    private CultureInfo _cultureInfo = new("ru-RU");

    public void OnChangeTime(DateTime time)
    {
        _currentTime = time;
    }
    public void HandleEditButtonClick()
    {
        _timeInput.gameObject.SetActive(true);
        _timeInput.text = _currentTime.ToString("HH:mm:ss");
        _edit.transform.DOScaleX(0, 0.5f);
        _save.SetActive(true);
        _save.transform.DOMoveX(6f, 0.5f);
    }

    public void HandleSaveButtonClick()
    {
        if (DateTime.TryParseExact(_timeInput.text, _timeFormat, _cultureInfo,
                DateTimeStyles.NoCurrentDateDefault, out var time))
        {
            if (_currentTime.Hour != time.Hour || _currentTime.Minute != time.Minute)
            {
                TimeEdit?.Invoke(time);
            }
            
            HandleCancelButtonClick();
        }
        else
        {
            Debug.Log("Invalid time format");
        }
    }

    public void HandleCancelButtonClick()
    {
        _save.transform.DOMoveX(9, 0.5f).OnComplete(() => _save.SetActive(false));
        _edit.transform.DOScaleX(1, 0.5f);
        _timeInput.gameObject.SetActive(false);
    }
}