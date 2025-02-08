using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HourHand : MonoBehaviour
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
            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90;
            if (angle > 0)
            {
                angle -= 360;
            }
            _transform.localRotation = Quaternion.Euler(0, 0, angle);
            int hours = Mathf.RoundToInt(-angle / GlobalConstants.ConstantsForTime.GRADUS_FOR_HOUR);
            _editedTime = _currentTime.AddHours(hours - _currentTime.Hour);
            
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
            float hourAngle = (_currentTime.Hour % GlobalConstants.ConstantsForTime.COUNT_HOURS
                               + _currentTime.Minute / GlobalConstants.ConstantsForTime.COUNT_MINUTES_AND_SECONDS) 
                              * GlobalConstants.ConstantsForTime.GRADUS_FOR_HOUR; 

            _transform.DOLocalRotate(Quaternion.Euler(0, 0, -hourAngle).eulerAngles, 
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
