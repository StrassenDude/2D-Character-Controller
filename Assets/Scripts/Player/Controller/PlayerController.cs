using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputReader _inputReader;

    [SerializeField] private float speed;

    private Vector2 _moveDirection;

    private bool _isFireing;


    // Start is called before the first frame update
    void Start()
    {
        _inputReader.MoveEvent += HandleMove;

        _inputReader.FireEvent += HandleFire;
        _inputReader.FireCancelEvent += HandleCancelFire;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        
    }


    private void HandleMove(Vector2 dir)
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

    private void Move()
    {
        if(_moveDirection == Vector2.zero)
        {
            return;
        }

        transform.position += new Vector3(_moveDirection.x, _moveDirection.y, 0) * (speed * Time.deltaTime);
    }


    private void Fire()
    {
        if (_isFireing)
        {
            Debug.Log("Fire");
        }
    }
}
