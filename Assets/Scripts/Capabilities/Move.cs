using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
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
    public void SetInputController(InputController ic) { input = ic;}
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
        _direction.x = input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);
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
}
