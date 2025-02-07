using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class TimeViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    

    public void OnChangeTime(DateTime time)
    {
        _timeText.text = time.ToString("HH:mm:ss");
    }

    
}