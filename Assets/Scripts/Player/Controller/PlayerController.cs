using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputReader _inputReader;

    [SerializeField] private float speed;

    private Vector2 _moveDirection;

    private bool _isFireing;

    [SerializeField] private bool _isAccelerating;

    public Rigidbody2D _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _inputReader.DirectionEvent += HandleDirection;

        _inputReader.FireEvent += HandleFire;
        _inputReader.FireCancelEvent += HandleCancelFire;

        _inputReader.AccelerateEvent += HandleAcceleration;
        _inputReader.AccelerateCancelEvent += HandleCancelAcceleration;
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void HandleAcceleration()
    {
        _isAccelerating = true;
    }

    private void HandleCancelAcceleration()
    {
        _isAccelerating = false;
    }

    private void HandleCancelFire()
    {
        _isFireing = false;
    }



    private void Accelerate()
    {

    }


    private void ApplyEngineForce()
    {
        
    }


    private void Fire()
    {
        if (_isFireing)
        {
            Debug.Log("Fire");
        }
    }
}
