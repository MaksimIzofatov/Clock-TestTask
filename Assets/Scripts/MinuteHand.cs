using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class MinuteHand : MonoBehaviour
{
    public event Action<DateTime> MouseMove;
    
    private Transform _transform;
    private DateTime _currentTime;
    private DateTime _editedTime;
    private bool _isEdit;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnMouseDrag()
    {
        if (_isEdit)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 directionToMouse = mousePosition - _transform.position;
            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg -90;
            if (angle > 0)
            {
                angle -= 360;
            }
            _transform.localRotation = Quaternion.Euler(0, 0, angle);
            int minutes = Mathf.RoundToInt(-angle / GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND);

            _editedTime = _currentTime.AddMinutes(minutes - _currentTime.Minute);
            Debug.Log(_editedTime.ToString("HH:mm:ss"));
            
        }
    }

    public void OnSave()
    {
        Debug.Log("save" + _editedTime.ToString("HH:mm:ss"));
        MouseMove?.Invoke(_editedTime);
    }
    
    private void Rotate()
    {
        if (!_isEdit)
        {
            float minuteAngle = (_currentTime.Minute +
                                 _currentTime.Second / GlobalConstants.ConstantsForTime.COUNT_MINUTES_AND_SECONDS)
                                * GlobalConstants.ConstantsForTime.GRADUS_FOR_MINUTE_AND_SECOND;

            _transform.DOLocalRotate(Quaternion.Euler(0, 0, -minuteAngle).eulerAngles,
                GlobalConstants.ConstantsForTime.TICK);
        }

    }
    
    public void OnIsEdit(bool isEdit)
    {
        _isEdit = isEdit;
    }
    
    public void OnChangeTime(DateTime time)
    {
        _currentTime = time;
        Rotate();
    }
}
