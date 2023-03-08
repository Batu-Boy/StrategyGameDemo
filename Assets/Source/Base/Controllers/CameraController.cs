using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

//TODO: implement camera controls
public class CameraController : ControllerBase
{
    [Header("Movement Options")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _speedBoost;

    [Header("Zoom Options")]
    [SerializeField] private float _maxZoom;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _zoomSensitivity;
    
    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _camTarget;
    
    private float _maxY;
    private float _minY;
    private float _maxX;
    private float _minX;
    
    private float _aspectRatio;
    private float _halfGridSize = 32;
    
    private void Start()
    {
        _aspectRatio = Screen.width / (float)Screen.height;
        ReArrangeOrthoSize();
    }


    public override void ControllerLateUpdate(GameStates currentState)
    {
        if(currentState != GameStates.Game) return;
        
        Zoom();
        Move();
    }

    private void Zoom()
    {
        if(Input.mouseScrollDelta.magnitude < .1f) return;
        if(EventSystem.current.IsPointerOverGameObject()) return;

        ReArrangeOrthoSize();
    }

    private void ReArrangeOrthoSize()
    {
        float newValue = _virtualCamera.m_Lens.OrthographicSize - Input.mouseScrollDelta.y * _zoomSensitivity;
        float clampedZoom = Mathf.Clamp(newValue, _minZoom, _maxZoom);
        _virtualCamera.m_Lens.OrthographicSize = clampedZoom;
        _maxY = _halfGridSize * 2 - clampedZoom - .5f;
        _minY = clampedZoom - .5f;

        _maxX = _halfGridSize + (_maxY - _halfGridSize) / _aspectRatio - .5f;
        _minX = _halfGridSize * 2 - _maxX -.5f;
    }

    private void Move()
    {
        var currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _moveSpeed + _speedBoost : _moveSpeed;
        var direction = GetDirection();
        var newValue = _camTarget.position + Time.deltaTime * currentSpeed * direction;
        newValue.y = Mathf.Clamp(newValue.y, _minY, _maxY);
        newValue.x = Mathf.Clamp(newValue.x, _minX, _maxX);
        _camTarget.position = newValue;
    }

    private void Reset()
    {
        _moveSpeed = 10;
        _speedBoost = 10;
        _maxZoom = 12;
        _minZoom = 5;
        _zoomSensitivity = .5f;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        return direction.normalized;
    }
}
