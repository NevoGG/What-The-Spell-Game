using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class Move2 : Move
{
    private PlayerInput1 controls;


    public override void SetMaxAirJumps(int h) { _maxAirJumps = h;}



    private void Awake()
    {
        // player = GetComponent<Animal>().GetPlayer();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        animator.SetFloat("Speed", Mathf.Abs(_body.velocity.x));
        animator.SetBool("IsDownPressed", _isDownPressed);
        animator.SetFloat("YVelocity", _body.velocity.y);
        //controls setup
        controls = new PlayerInput1();
        
        _body.drag = _groundLinearDrag;
        transform.localScale = transform.localScale * _size;
        //speed of falling while down key is pressed:
        _downardMovementWithoutPress = _downwardMovementMultiplier;
        _downardMovementOnPress = _downwardOnPressMultiplier * _downwardMovementMultiplier;
        UpdateParams();
    }

    private void Start()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y) * _size;
    }


    private void Update()
    {
        if (isDashing) return;
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction * Time.deltaTime, 0f);
        
        //Animator control parameters:
        animator.SetFloat(Speed, Mathf.Abs(_body.velocity.x));
        animator.SetBool(IsJumping, !_ground.OnGround);
        // animator.SetBool("IsDashing", !isDashing);
        if (isFacingRight && _direction.x < 0) Flip();
        if (!isFacingRight && _direction.x > 0) Flip();
        
        //Jump and linear drag:
        if (!_onGround)
        {
            if (_isDownPressed)
            {
                _body.velocity = new Vector2(_body.velocity.x, _downwardOnPressMultiplier);
            }
            //set downward movement speed:
            // _downwardMovementMultiplier = _isDownPressed ? _downardMovementOnPress : _downardMovementWithoutPress;
            //set linear drag:
            _body.drag = (_body.velocity.y > 0) ? _upwardLinearDrag : _downardLinearDrag;
        }
        else _body.drag = _groundLinearDrag;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
            _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;
        
        //Jump actions:

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (_body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if(_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }
        _body.velocity = _velocity;
    }
    
}
