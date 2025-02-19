using System;
using TMPro;
using UnityEngine;

public class TimeViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    
    public void OnChangeTime(DateTime time)
    {
        _timeText.text = time.ToString("HH:mm:ss");
    }
}