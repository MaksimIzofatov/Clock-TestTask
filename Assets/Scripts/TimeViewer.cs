using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class TimeViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _edit;
    [SerializeField] private GameObject _save;

    public void OnChangeTime(DateTime time)
    {
        _timeText.text = time.ToString("HH:mm:ss");
    }

    public void OnClickEditButton()
    {
        _edit.transform.DOScaleX(0, 0.5f);
        _save.SetActive(true);
        _save.transform.DOMoveX(700, 0.5f);
    }

    public void OnClickCancelButton()
    {
        _save.transform.DOMoveX(1150, 0.5f);
        _edit.transform.DOScaleX(1, 0.5f);
    }

    private void SwitchActive()
    {
        _save.SetActive(false);
    }
}