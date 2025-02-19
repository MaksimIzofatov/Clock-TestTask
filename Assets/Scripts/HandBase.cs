using System;
using UnityEngine;


public abstract class HandBase : MonoBehaviour
{
    public event Action<DateTime> ChangeTimeWithMouse;
    
    protected const int TICK = 1;
    protected const int GRADUS_FOR_HOUR = 30;
    protected const int GRADUS_FOR_MINUTE_AND_SECOND = 6;
    protected const float COUNT_MINUTES_AND_SECONDS = 60;

    protected Transform _transform;
    protected DateTime _currentTime;
    protected DateTime _editedTime;
    protected bool _isEdit;

    private const int COEFFICIENT_ANGLE_MOVE = 90;
    private const int COEFFICIENT_ANGLE = 360;
    
    private Camera _camera;
    private bool _wasEdited;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    public void OnSave()
    {
        if (_wasEdited)
        {
            _wasEdited = false;
            _editedTime = _editedTime.AddSeconds(_currentTime.Second - _editedTime.Second);
            ChangeTimeWithMouse?.Invoke(_editedTime);
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

    protected void EditTime()
    {
        if (_isEdit == false) return;
        
        _wasEdited = true;
        _editedTime = _currentTime;
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 directionToMouse = mousePosition - _transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - COEFFICIENT_ANGLE_MOVE;
            
        if (angle > 0)
        {
            angle -= COEFFICIENT_ANGLE;
        }

        _transform.localRotation = Quaternion.Euler(0, 0, angle);
        SetTime(angle);
    }

    protected virtual void SetTime(float angle){}

    protected abstract void Rotate();
}