using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Plane Settings")]
    [SerializeField] private float setAccelerationFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float turnFactorWithEngine;
    [SerializeField] private float turnFactorNoEngine;
    [SerializeField] private float driftFactorWithEngine;
    [SerializeField] private float driftFactorNoEngine;
    [SerializeField] private float dragFactor;
    [SerializeField] private float gravityFactor;

    [SerializeField] private bool _isFireing;
    [SerializeField] private bool _isAccelerating;

    [SerializeField] private PrimaryWeapon _primaryWeapon;

    public Transform _primaryWeaponTransform;


    [Header("RigidBody2D")]
    public Rigidbody2D _rigidbody;

    [Header("TrailRenderer")]
    public TrailRenderer _trailRenderer;

    [Header("ParticleSystem")]
    public ParticleSystem _particleSystem;

    [Header("Input Reader")]
    [SerializeField] private InputReader _inputReader;


    //Lokale Variablen
   

    private float turnFactor;
    private float steeringInput;
    private float accelerationFactor;
    private float driftFactor;

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
        HandleEngine();
        ApplyGravity();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSmoke();
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


    private void HandleSmoke()
    {
        if (_isAccelerating)
        {
            _trailRenderer.emitting = true;
        }
        else if (!_isAccelerating)
        {
            _trailRenderer.emitting = false;
        }

        if (_isAccelerating && accelerationFactor == 0)
        {
            _particleSystem.Play();
        }
    }



    private void HandleEngine()
    {
        if (_isAccelerating)
        {
            accelerationFactor = setAccelerationFactor;
            driftFactor = driftFactorWithEngine;
            turnFactor = turnFactorWithEngine;
        }
        else if (!_isAccelerating)
        {
            accelerationFactor = 0;
            driftFactor = driftFactorNoEngine;
            turnFactor = turnFactorNoEngine;
        }
    }

    private void ApplyEngineForce()
    {
        // Calcutates how much "forword" the plane is going in terms of direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);

        // Applies Drag when Acceleration is not pressed and slows down vehicle
        if (accelerationFactor == 0)
        {
            _rigidbody.drag = Mathf.Lerp(_rigidbody.drag, dragFactor, Time.fixedDeltaTime * 3);
        }
        else
        {
            _rigidbody.drag = 0;
        }

        //limit that we cannot go faster than max speed in "forward" direction
        if (velocityVsUp > maxSpeed && _isAccelerating)
        {
            return;
        }

        // Creates Force with the engine
        Vector2 engineForceVector = transform.up * accelerationFactor;

        // Applies Force and pushes the plane forware
        _rigidbody.AddForce(engineForceVector, ForceMode2D.Force);

    }


    private void ApplySteering()
    {
        steeringInput = _moveDirection.y;

        // Updates Rotation Angle based on Input
        rotationAngle -= steeringInput * turnFactor;

        // Apply steering by rotating Plane Object
        _rigidbody.MoveRotation(rotationAngle);
    }


    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody.velocity, transform.right);

        _rigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private void ApplyGravity()
    {
        if (!_isAccelerating)
        {
            StartCoroutine(CalculateGravity());
        }
        else _rigidbody.gravityScale = 0;
    }

    IEnumerator CalculateGravity()
    {
        yield return new WaitForSeconds(0.8f);
        _rigidbody.gravityScale = Mathf.Lerp(_rigidbody.gravityScale, gravityFactor, Time.fixedDeltaTime * 3);
    }


    private void Fire()
    {
        if (_isFireing)
        {
            _primaryWeapon.Fire(_primaryWeaponTransform);
            _isFireing = false;
        }
    }
}
