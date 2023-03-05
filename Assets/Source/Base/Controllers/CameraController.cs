using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

//TODO: implement camera controls
public class CameraController : ControllerBase
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private BoxCollider2D boundingBox;
    [SerializeField] private float maxZoom;
    [SerializeField] private float minZoom;
    [SerializeField] private float _zoomSensitivity;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _camTarget;
    
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    
    private float aspectRatio;
    private float gridSize = 32;
    
    private void Start()
    {
        aspectRatio = Screen.width / (float)Screen.height;
        ReArrangeOrthoSize();
    }

    private void Update()
    {
        Zoom();
        Move();
    }

    private void Zoom()
    {
        if(Input.mouseScrollDelta.magnitude < .1f) return;

        ReArrangeOrthoSize();
    }

    private void ReArrangeOrthoSize()
    {
        float newValue = _virtualCamera.m_Lens.OrthographicSize - Input.mouseScrollDelta.y * _zoomSensitivity;
        float clampedZoom = Mathf.Clamp(newValue, minZoom, maxZoom);
        _virtualCamera.m_Lens.OrthographicSize = clampedZoom;
        maxY = gridSize * 2 - clampedZoom - .5f;
        minY = clampedZoom - .5f;

        maxX = gridSize + (maxY - gridSize) / aspectRatio - .5f;
        minX = gridSize * 2 - maxX -.5f;
    }

    private void Move()
    {
        var direction = GetDirection();
        var newValue = _camTarget.position + Time.deltaTime * _moveSpeed * direction;
        newValue.y = Mathf.Clamp(newValue.y, minY, maxY);
        newValue.x = Mathf.Clamp(newValue.x, minX, maxX);
        _camTarget.position = newValue;
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
