using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class Move4 : Move
{
    private PlayerInput4 controls; 
    // private InputAction move;
    // private InputAction jump;
    // private InputAction crouch;
    // private InputAction power;
    // //animation parameters: 
    // public bool isFacingRight = true;
    //
    // //Movement Fields:
    // [SerializeField, Range(0f, 1000f)] private float _maxSpeed = 15f;
    // [SerializeField, Range(0f, 300f)] private float _maxAcceleration = 35f;
    // [SerializeField, Range(0f, 1000f)] private float _maxAirAcceleration = 20f;
    // [SerializeField, Range(0f, 10)] private float _downwardOnPressMultiplier = 2f;	
    //
    // //Jump Fields:
    // [SerializeField, Range(0f, 20f)] private float _downwardMovementMultiplier = 3f;
    // [SerializeField, Range(0f, 20f)] private float _upwardMovementMultiplier = 1.7f;
    // [SerializeField, Range(0f, 500f)] private float _jumpHeight = 100f;
    //
    // //linear drag:
    // [SerializeField, Range(0f, 10f)] private float  _groundLinearDrag= 10f;
    // [SerializeField, Range(0f, 10f)] private float  _upwardLinearDrag= 10f;
    // [SerializeField, Range(0f, 10f)] private float  _downardLinearDrag= 10f;
    //
    // [SerializeField, Range(0f, 15f)] private float _size = 10f;
    //
    // private int _maxAirJumps = 0;
    // private int _jumpPhase;
    // private float _downardMovementOnPress;
    // private float _downardMovementWithoutPress;
    //
    // //todo: changed, were no default values
    // private float _defaultGravityScale = 1;
    // private float _jumpSpeed = 1;
    // private float _maxSpeedChange, _acceleration;
    // private bool _isDownPressed = false;
    //
    //
    // private bool _desiredJump, _onGround;
    //
    // // private Controller _controller;
    // private Vector2 _direction, _desiredVelocity, _velocity;
    // private Rigidbody2D _body;
    // private BoxCollider2D _boxCollider;
    // private Ground _ground;
    // private Animator animator;



    //Setters:
    public void SetMaxSpeed(float h) { _maxSpeed = h;}
    public void SetMaxAcceleration(float h) { _maxAcceleration = h;}
    public void SetMaxAirAcceleration(float h) { _maxAirAcceleration = h;}
    public void SetJumpHeight(float h) { _jumpHeight = h;}
    public override void SetMaxAirJumps(int h) { _maxAirJumps = h;}
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
        _boxCollider = GetComponent<BoxCollider2D>();
        animator.SetFloat("Speed", Mathf.Abs(_body.velocity.x));
        animator.SetBool("IsDownPressed", _isDownPressed);
        animator.SetFloat("YVelocity", _body.velocity.y);
        //controls setup
        controls = new PlayerInput4();
        
        _body.drag = _groundLinearDrag;
        transform.localScale = transform.localScale * _size;
        _boxCollider.size = Vector2.one * 0.8f;
        //speed of falling while down key is pressed:
        _downardMovementWithoutPress = _downwardMovementMultiplier;
        _downardMovementOnPress = _downwardOnPressMultiplier * _downwardMovementMultiplier;
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
        Debug.Log("jump");
    }
    
    private void Crouch(InputAction.CallbackContext context)
    {
        _isDownPressed = true;
        Debug.Log("crouch");
    }
    
    private void CrouchCanceled(InputAction.CallbackContext context)
    {
        _isDownPressed = false;
        Debug.Log("crouch canceled");
    }
    
    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _desiredJump = false;
    }

    private void PassThroughPlatform(InputAction.CallbackContext context)
    {
        Debug.Log("PassThroughPlatform");
    }
    
    // private void CancelJump(InputAction.CallbackContext context)
    // {
    //     Debug.Log("jumped")
    // }
    
}
