using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimeEditer : MonoBehaviour
{
    public event Action<DateTime> TimeEdit;
    
    [SerializeField] private GameObject _edit;
    [SerializeField] private GameObject _save;
    [SerializeField] private TMP_InputField _timeInput;
    
    private DateTime _time;
    
    public void OnChangeTime(DateTime time)
    {
        _time = time;
    }
    public void OnClickEditButton()
    {
        _timeInput.gameObject.SetActive(true);
        _timeInput.text = _time.ToString("HH:mm:ss");
        _edit.transform.DOScaleX(0, 0.5f);
        _save.SetActive(true);
        _save.transform.DOMoveX(0.9f, 0.5f);
    }

    public void OnClickSaveButton()
    {
        
        if(DateTime.TryParse(_timeInput.text, out var time))
        {
            _timeInput.gameObject.SetActive(false);
            OnClickCancelButton();
            TimeEdit?.Invoke(time);
        }
        else
        {
            Debug.Log("Invalid time format");
        }
    }

    public void OnClickCancelButton()
    {
        _save.transform.DOMoveX(3, 0.5f).OnComplete(() => _save.SetActive(false));
        _edit.transform.DOScaleX(1, 0.5f);
        _timeInput.gameObject.SetActive(false);
    }
}
