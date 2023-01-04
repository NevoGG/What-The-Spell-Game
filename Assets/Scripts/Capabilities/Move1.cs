using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class Move1 : Move
{
    private PlayerInput1 controls; 

    public override void SetMaxAirJumps(int h) { _maxAirJumps = h;}

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void Awake()
    {
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
    }

    private void Start()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }


    private void Update()
    {
        _direction.x = move.ReadValue<float>();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction * Time.deltaTime, 0f);
        
        //Animator control parameters:
        animator.SetFloat("Speed", Mathf.Abs(_body.velocity.x));
        animator.SetBool("IsJumping", !_ground.OnGround);
        if (isFacingRight && _direction.x < 0) Flip();
        if (!isFacingRight && _direction.x > 0) Flip();
        
        //Jump and linear drag:
        if (!_onGround)
        {
            //set downward movement speed:
            _downwardMovementMultiplier = _isDownPressed ? _downardMovementOnPress : _downardMovementWithoutPress;
            //set linear drag:
            _body.drag = (_body.velocity.y > 0) ? _upwardLinearDrag : _downardLinearDrag;
        }
        else _body.drag = _groundLinearDrag;
    }

    private void FixedUpdate()
    {
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

    private void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();
        jump = controls.Player.Jump;
        jump.Enable();
        crouch = controls.Player.Crouch;
        crouch.Enable();
        jump.performed += Jump;
        jump.canceled += JumpCanceled;
        crouch = controls.Player.Crouch;
        crouch.performed += context =>
        {
            if (context.interaction is HoldInteraction) Crouch(context);;
            if (context.interaction is MultiTapInteraction) PassThroughPlatform(context);
        };
        crouch.canceled += CrouchCanceled;
        power =controls.Player.Power;
        power.Enable();
        // power.performed += Power; todo: when adding power
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        power.Disable();
        crouch.Disable();
    }

    private void JumpAction()
    {
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase += 1;
                
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
                
            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_body.velocity.y);
            }
            _velocity.y += _jumpSpeed;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _desiredJump = true;
    }
    
    private void Crouch(InputAction.CallbackContext context)
    {
        _isDownPressed = true;
    }
    
    private void CrouchCanceled(InputAction.CallbackContext context)
    {
        _isDownPressed = false;
    }
    
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _desiredJump = false;
    }

    private void PassThroughPlatform(InputAction.CallbackContext context)
    {
        _ground.PassCurPlatform(); //todo: put in move instead of move1
    }

}
