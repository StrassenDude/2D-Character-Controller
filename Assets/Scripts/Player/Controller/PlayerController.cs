using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Plane Settings")]
    [SerializeField] private float setAccelerationFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float turnFactor;
    [SerializeField] private float driftFactor;

    [SerializeField] private bool _isFireing;
    [SerializeField] private bool _isAccelerating;


    [Header("RigidBody2D")]
    public Rigidbody2D _rigidbody;


    [Header("Input Reader")]
    [SerializeField] private InputReader _inputReader;


    //Lokale Variablen
    private float steeringInput;
    private float accelerationFactor;

    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    private Vector2 _moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        _inputReader.DirectionEvent += HandleDirection;

        _inputReader.FireEvent += HandleFire;
        _inputReader.FireCancelEvent += HandleCancelFire;

        _inputReader.AccelerateEvent += HandleAcceleration;
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        HandleEngine();
        Fire();
    }


    private void HandleDirection(Vector2 dir)
    {
        _moveDirection = dir;
    }

    private void HandleFire()
    {
        _isFireing = true;
    }

    private void HandleCancelFire()
    {
        _isFireing = false;
    }
    private void HandleAcceleration()
    {
        _isAccelerating = !_isAccelerating;
    }



    private void HandleEngine()
    {
        if (_isAccelerating)
        {
            accelerationFactor = setAccelerationFactor;
        }
        else if (!_isAccelerating)
        {
            accelerationFactor = 0;
        }
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);

        //limit that we cannot go faster than max speed in "forward" direction
        if (velocityVsUp > maxSpeed && _isAccelerating)
        {
            return;
        }


        Vector2 engineForceVector = transform.up * accelerationFactor;

        _rigidbody.AddForce(engineForceVector, ForceMode2D.Force);

    }

    private void ApplySteering()
    {
        steeringInput = _moveDirection.x;

        rotationAngle -= steeringInput * turnFactor;

        _rigidbody.MoveRotation(rotationAngle);
    }


    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody.velocity, transform.right);

        _rigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }


    private void Fire()
    {
        if (_isFireing)
        {
            Debug.Log("Fire");
        }
    }
}
