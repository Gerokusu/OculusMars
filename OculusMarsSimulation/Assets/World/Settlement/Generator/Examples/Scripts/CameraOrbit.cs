using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;


using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float Distance = 10.0f;
    public float ScrollSensitivity = 200f;

    public float XSpeed = 250.0f;
    public float YSpeed = 120.0f;

    public float YMinLimit = -20f;
    public float YMaxLimit = 80f;

    public float DistanceMin = 3f;
    public float DistanceMax = 15f;

    public static CameraOrbit Instance;

    private float x = 0.0f;
    private float y = 0.0f;
    private Vector3 position;

    private float _targetDistance;
    private float _t = 0;
    private bool _interpolate = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        UpdatePosition();

        Time.timeScale = 1f;
    }

    void Update()
    {
        UpdatePosition();
    }

    public void SetTargetDistance(float targetDistance)
    {
        Distance = targetDistance;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        if (target == null)
            return;

        if ((Input.GetMouseButton(1) || Input.GetAxis("Mouse ScrollWheel") != 0 || _interpolate) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * XSpeed * 0.035f;
                y -= Input.GetAxis("Mouse Y") * YSpeed * 0.045f;
                y = ClampAngle(y, YMinLimit, YMaxLimit);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
                Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity, DistanceMin, DistanceMax);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -Distance) + target.position;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}