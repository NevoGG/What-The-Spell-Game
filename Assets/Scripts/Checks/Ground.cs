using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool OnGround { get; private set; }
    public float Friction { get; private set; }
    
    //todo: delete later:
    [SerializeField] private float bgMovementSpeed;
    private Vector3 _lastLocation;

    private bool _isTriggerOn = false;
    private Vector2 _normal;
    private PhysicsMaterial2D _material;
    private Move _move;
    private Collider2D _collider;
    private GameObject curOneWayPlatform;
    private float _timeToWait = 0.5f; //todo: change to something good for every player.;
    private double _threshold = 0.0001;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // private void Update()
    // {
    //     List<Collider2D> collider2Ds = new List<Collider2D>();
    //     _collider.GetContacts(collider2Ds);
    //     if (collider2Ds.Count == 0 && _isTriggerOn == false)
    //     {
    //         OnGround = false; //todo: patch, delete later
    //     }
    // }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.PLATFORM_TAG)
            || collision.gameObject.CompareTag("OneWayPlatform")
            || collision.gameObject.CompareTag("Player"))
        {
            OnGround = false;
            Friction = 0;
        }

        if (collision.gameObject.CompareTag("OneWayPlatform")) //todo: no magic numbers
        {
            curOneWayPlatform = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.PLATFORM_TAG)
            || collision.gameObject.CompareTag("OneWayPlatform")
            || collision.gameObject.CompareTag("Player"))
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);  
        }
        if (collision.gameObject.CompareTag("OneWayPlatform")) //todo: no magic numbers
        {
            curOneWayPlatform = collision.gameObject;
        }
    }
    // // /// <summary>
    // // /// Patch as fuck todo: change.
    // // /// </summary>
    // // /// <param name="col"></param>
    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag(GameManager.PLATFORM_TAG)
    //         || col.gameObject.CompareTag("OneWayPlatform"))
    //         {
    //             OnGround = true;
    //             _isTriggerOn = true;
    //         }
    // }
    //
    // private void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag(GameManager.PLATFORM_TAG)
    //         || col.gameObject.CompareTag("OneWayPlatform"))
    //     {
    //         OnGround = true;
    //         _isTriggerOn = true;
    //     }
    // }
    //
    // private void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.CompareTag(GameManager.PLATFORM_TAG)
    //         || col.gameObject.CompareTag("OneWayPlatform"))
    //     {
    //         OnGround = false;
    //         _isTriggerOn = false;
    //     }
    // }
    /// <summary>
    /// ///patch ends
    /// </summary>
    /// <param name="collision"></param>

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameManager.PLATFORM_TAG)
            || collision.gameObject.CompareTag("OneWayPlatform")
            || collision.gameObject.CompareTag("Player"))
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);  
        }

        if (collision.gameObject.CompareTag("OneWayPlatform")) //todo: no magic numbers
        {
            curOneWayPlatform = collision.gameObject;
        }
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            _normal = collision.GetContact(i).normal;
            OnGround |= _normal.y >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        _material = collision.rigidbody.sharedMaterial;

        Friction = 0;

        if(_material != null)
        {
            Friction = _material.friction;
        }
    }

    public void PassCurPlatform()
    {
        if (curOneWayPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = curOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(_collider, platformCollider);
        yield return new WaitForSeconds(_timeToWait);
        Physics2D.IgnoreCollision(_collider, platformCollider, false);
    }
}
