using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private InputAction playerControls;

    private float _maxSpeed = 4f;
    private float _maxAcceleration = 35f;
    private float _maxAirAcceleration = 20f;

    // private Controller _controller;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private Ground _ground;

    private float _maxSpeedChange, _acceleration;
    private bool _onGround;

    //Setters:
    public void SetMaxSpeed(float h) { _maxSpeed = h;}
    public void SetMaxAcceleration(float h) { _maxAcceleration = h;}
    public void SetMaxAirAcceleration(float h) { _maxAirAcceleration = h;}
    public void SetRigidBody(Rigidbody2D rb) { _body = rb;}
    
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        // _controller = GetComponent<Controller>();
    }

    private void Update()
    {
        _direction.x = playerControls.ReadValue<float>();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction * Time.deltaTime, 0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;
    }

    private void OnEnable() { playerControls.Enable(); }

    private void OnDisable() { playerControls.Disable();}
}
