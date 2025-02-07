using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouse : MonoBehaviour
{
    public event Action<bool, Vector3> MouseMove;

    private bool _isDragMouse;
    private bool _isHourHand;
    private bool _isEdit;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (_isEdit && hit.collider.gameObject.CompareTag("HourHand"))
            {
                _isDragMouse = true;
                _isHourHand = true;
            }
            else if (_isEdit && hit.collider.gameObject.CompareTag("MinuteHand"))
            {
                _isDragMouse = true;
            }
            
            if (_isDragMouse)
                MouseMove?.Invoke(_isHourHand, hit.point);
        }

        
    }

    private void OnMouseDrag(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if (_isEdit && collider.gameObject.CompareTag("HourHand"))
        {
            _isDragMouse = true;
            _isHourHand = true;
        }
        else if (_isEdit && collider.gameObject.CompareTag("MinuteHand"))
        {
            _isDragMouse = true;
        }
    }

    private void OnMouseDown()
    {
        _isDragMouse = false;
        _isHourHand = false;
    }

    public void OnIsEdit(bool isEdit)
    {
        _isEdit = isEdit;
    }
    
}
