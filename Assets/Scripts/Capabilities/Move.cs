using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private PlayerInput1 controls; 
    private InputAction move;
    private InputAction jump;
    private InputAction power;
    //animation parameters: 
    public bool isFacingRight = true;
    
    private float _maxSpeed = 4f;
    private float _maxAcceleration = 35f;
    private float _maxAirAcceleration = 20f;

    private float _jumpHeight = 3f;
    private int _maxAirJumps = 0;
    private float _downwardMovementMultiplier = 3f;
    private float _upwardMovementMultiplier = 1.7f;

    private int _jumpPhase;
    
    //todo: changed, were no default values
    private float _defaultGravityScale = 1;
    private float _jumpSpeed = 1;

    private bool _desiredJump, _onGround;

    // private Controller _controller;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private Ground _ground;
    private Animator animator;

    private float _maxSpeedChange, _acceleration;

    //Setters:
    public void SetMaxSpeed(float h) { _maxSpeed = h;}
    public void SetMaxAcceleration(float h) { _maxAcceleration = h;}
    public void SetMaxAirAcceleration(float h) { _maxAirAcceleration = h;}
    public void SetJumpHeight(float h) { _jumpHeight = h;}
    public void SetMaxAirJumps(int h) { _maxAirJumps = h;}
    public void SetDownwardMovementMultiplier(float h) { _downwardMovementMultiplier = h;}
    public void SetUpwardMovementMultiplier(float h) { _upwardMovementMultiplier = h;}
    
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
        animator.SetFloat("Speed", Mathf.Abs(_body.velocity.x));
        animator.SetFloat("YVelocity", _body.velocity.y);
        //controls setup
        controls = new PlayerInput1();
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
        Debug.Log(_body.gravityScale); //todo: delete
    }

    private void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();
        jump = controls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
        jump.canceled += JumpCanceled;
        power =controls.Player.Power;
        power.Enable();
        // power.performed += Power; todo: when adding power
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        power.Disable();
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
    
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _desiredJump = false;
    }
    
    // private void CancelJump(InputAction.CallbackContext context)
    // {
    //     Debug.Log("jumped")
    // }
    
}
