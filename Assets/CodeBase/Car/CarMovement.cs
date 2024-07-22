using Fusion;
using System;
using UnityEngine;
using Zenject;

[Serializable]
public struct Wheel
{
    public Transform WheelModel;
    public WheelCollider WheelCollider;
    public bool IsForwardWheel;

    public void UpdateModel()
    {
        Vector3 position;
        Quaternion rotation;

        WheelCollider.GetWorldPose(out position, out rotation);

        WheelModel.position = position;
        WheelModel.rotation = rotation;
    }
}

public class CarMovement : NetworkBehaviour
{
    [SerializeField] private Wheel[] _wheels;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _morotPower;
    [SerializeField] private float _brakePower;
    [SerializeField] private float _brakeInput;
    [SerializeField] private AnimationCurve _steeringCurve;

    private float _verticalInput;
    private float _hoirzontalInput;

    private float _speed;

    private void Start()
    {
        if (Object.HasInputAuthority)
        {
            CameraVirtual.Instance.VirtualCamera.Follow = transform;
            UITopDriftPanel.Instance.DriftTarget = _rigidbody;
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Move();
        Brake();
        Steering();
        CheckInput();
    }

    private void Move()
    {
        _speed = _rigidbody.velocity.magnitude;

        foreach (Wheel wheel in _wheels)
        {
            wheel.WheelCollider.motorTorque = _morotPower * _verticalInput;
            wheel.UpdateModel();
        }
    }

    private void CheckInput()
    {
        if (GetInput(out NetworkInputData data))
        {
            _hoirzontalInput = data.HorizontalInput;
            _verticalInput = data.VerticalInput;

            float movingDirectional = Vector3.Dot(transform.forward, _rigidbody.velocity);

            if ((movingDirectional < -0.5f && _verticalInput > 0) || (movingDirectional > 0.5f && _verticalInput < 0))
            {
                _brakeInput = Mathf.Abs(_verticalInput);
            }
            else if (data.BrakeInput)
            {
                _brakeInput = 1f;
            }
            else
            {
                _brakeInput = 0;
            }
        }
    }

    private void Brake()
    {
        foreach (Wheel wheel in _wheels)
        {
            if (wheel.IsForwardWheel)
                wheel.WheelCollider.brakeTorque = _brakeInput * _brakePower * 0.7f; 
            else
                wheel.WheelCollider.brakeTorque = _brakeInput * _brakePower * 0.3f;
        }
    }

    private void Steering()
    {
        float steeringAngle = _hoirzontalInput * _steeringCurve.Evaluate(_speed);
        float slipAngle = Vector3.Angle(transform.forward, _rigidbody.velocity - transform.forward);

        if (slipAngle < 120)
            steeringAngle += Vector3.SignedAngle(transform.forward, _rigidbody.velocity, Vector3.up);

        steeringAngle = Mathf.Clamp(steeringAngle, -48, 48);

        foreach (Wheel wheel in _wheels)
        {
            if (wheel.IsForwardWheel)
                wheel.WheelCollider.steerAngle = steeringAngle;
        }
    }
}