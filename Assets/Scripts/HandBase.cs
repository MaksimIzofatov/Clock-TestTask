using System;
using UnityEngine;


public abstract class HandBase : MonoBehaviour
{
    public event Action<DateTime> MouseMove;

    protected Transform _transform;
    protected Camera _camera;
    protected DateTime _currentTime;
    protected DateTime _editedTime;
    protected bool _isEdit;
    protected bool _wasEdited;

    private const int COEFFICIENT_ANGLE_MOVE = 90;
    private const int COEFFICIENT_ANGLE = 360;

    protected void EditTime()
    {
        if (_isEdit)
        {
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
    }

    protected virtual void SetTime(float angle){}

    public void OnSave()
    {
        if (_wasEdited)
        {
            _wasEdited = false;
            _editedTime = _editedTime.AddSeconds(_currentTime.Second - _editedTime.Second);
            MouseMove?.Invoke(_editedTime);
        }
    }

    protected abstract void Rotate();

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