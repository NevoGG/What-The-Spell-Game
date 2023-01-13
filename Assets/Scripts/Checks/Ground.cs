using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool OnGround { get; private set; }
    public float Friction { get; private set; }
    
    //todo: delete later:
    [SerializeField] private float bgMovementSpeed;
    private Vector3 _lastLocation;
    private Rigidbody2D _body;

    private bool _isTriggerOn = false;
    private Vector2 _normal;
    private PhysicsMaterial2D _material;
    private Move _move;
    private Collider2D _collider;
    private GameObject curOneWayPlatform;
    private float _timeToWait = 0.5f; //todo: change to something good for every player.;
    private double _threshold = 0.0001;
    private float _bounceFactor = 80f;
    private float _yPushVelocity= 40f;
    private float _pushFactor = 100f;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();
    }

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
        switch (collision.gameObject.tag)
        {
            case "OneWayPlatform":
                EvaluateCollision(collision);
                RetrieveFriction(collision);
                curOneWayPlatform = collision.gameObject;
                break;
            case "Platform":
                EvaluateCollision(collision);
                RetrieveFriction(collision);
                break;
            case "Player":
                CheckAndBouncePlayer(collision);
                break;
            case "Dashing":
                PushPlayer(collision);
                break;
            default: return;
        }
    }
    

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

    private void CheckAndBouncePlayer(Collision2D collision)
    {
        _normal = collision.GetContact(0).normal; //todo: possible bug
        if (_normal.y >= 0.9f)
        {
            _body.velocity = Vector2.up * _bounceFactor;
        }
    }

    private void PushPlayer(Collision2D collision)
    {
        float otherVelocityX = collision.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        int pushDir = otherVelocityX > 0 ? 1 : -1;
        _body.velocity = new Vector2(pushDir * _pushFactor, _yPushVelocity); //todo: push factor by animal
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            _normal = collision.GetContact(i).normal;
            OnGround |= (_normal.y >= 0.9f && _body.velocity.y < 0.001f);
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
