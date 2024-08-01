using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private LayerMask whatDestroysBullet;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        SetStraightVelocity();

        SetDestroyTime();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the collision within the whatDestroysBullet layerMask
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            //span particles

            //play sound FX

            //ScreenShkae

            //Damage Enemy

            //Destroy the bullet
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity()
    {
        _rigidbody.velocity = transform.up * bulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}


