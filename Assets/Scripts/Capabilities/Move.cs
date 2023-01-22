using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public abstract class Move : MonoBehaviour
{
    //Dash Params:
    protected bool canDash = true;
    protected bool isDashing;
    protected float dashPower = 40f;
    protected float dashTime = 0.2f;
    protected float dashCooldown = 1f;
    [SerializeField] protected TrailRenderer tr;
    
    protected PlayerInput1 controls; 
    protected InputAction move;
    protected InputAction jump;
    protected InputAction crouch;
    protected InputAction dash;
    protected InputAction power;
    //animation parameters: 
    protected bool isFacingRight = true;
    public ParticleSystem dust;
    
    public AnimalParams animalParams;
    
    //Movement Fields:
    protected float _maxSpeed = 15f;
    protected float _maxAcceleration = 35f;
    protected float _maxAirAcceleration = 20f;
    protected float _downwardOnPressMultiplier = 2f;	
    
    //Jump Fields:
    protected float _downwardMovementMultiplier = 3f;
    protected float _upwardMovementMultiplier = 1.7f;
    protected float _jumpHeight = 100f;
    protected float _multiJumpMultiplier = 0.75f;
    
    //linear drag:
    protected float  _groundLinearDrag= 10f;
    protected float  _upwardLinearDrag= 10f;
    protected float  _downardLinearDrag= 10f;
    protected float _size = 1f;
    
    protected int _jumpPhase;
    protected float _downardMovementOnPress;
    protected float _downardMovementWithoutPress;
    
    //todo: changed, were no default values
    protected float _defaultGravityScale = 1;
    protected float _jumpSpeed = 1;
    protected float _maxSpeedChange, _acceleration;
    protected bool _isDownPressed = false;

    protected bool _desiredJump, _onGround;
    
    // private Controller _controller;
    protected Vector2 _direction, _desiredVelocity, _velocity;
    protected Rigidbody2D _body;
    protected BoxCollider2D _boxCollider;
    protected Ground _ground;
    protected Animator animator;
    protected GameObject oneWayPlatform = null;
    
    protected int _maxAirJumps = 0;

    protected Player player;
    public abstract void SetMaxAirJumps(int k);
    
    protected static readonly int IsJumping = Animator.StringToHash("IsJumping");
    protected static readonly int Speed = Animator.StringToHash("Speed");
    
    protected void UpdateParams()
    {
    //Movement Fields:
    _maxSpeed = animalParams._maxSpeed;
    _maxAcceleration  = animalParams._maxAcceleration;
    _maxAirAcceleration = animalParams._maxAirAcceleration;
    _downwardOnPressMultiplier = animalParams._downwardOnPressMultiplier;	
    
    //Jump Fields:
    _downwardMovementMultiplier = animalParams._downwardMovementMultiplier;
    _upwardMovementMultiplier = animalParams._upwardMovementMultiplier;
    _jumpHeight = animalParams._jumpHeight;
    _multiJumpMultiplier = animalParams._multiJumpMultiplier;
    
    //linear drag:
    _groundLinearDrag = animalParams._groundLinearDrag;
    _upwardLinearDrag = animalParams._upwardLinearDrag;
    _downardLinearDrag = animalParams._downardLinearDrag;
    _size = animalParams._size;
    dashPower = animalParams._dashPower;
    _body.mass = animalParams._mass;
    
    //bounce factor: 
    GetComponent<Ground>()._bounceFactor = animalParams._bouncePower;
    }
    
    protected void CreateDust()
    {
        dust.Play();
    }

    protected IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        player.Dash();
        float originalDrag = _body.drag;
        float originalGravity = _body.gravityScale;
        string originalTag = tag;
        tag = "Dashing";
        _body.gravityScale = 0f;
        _body.drag = 2;
        _body.velocity = new Vector2(transform.localScale.x * dashPower * -1, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        _body.gravityScale = originalGravity;
        isDashing = false;
        tag = originalTag;
        _body.drag = originalDrag;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    
    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        CreateDust();
    }
    
    protected void JumpAction()
    {
        CreateDust();
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            player.Jump();
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
            _velocity.y += _jumpSpeed * (Mathf.Pow(_multiJumpMultiplier,_jumpPhase));
        }
    }
    
    protected void Jump(InputAction.CallbackContext context)
    {
        _desiredJump = true;
    }

    protected void DashFunc(InputAction.CallbackContext context)
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }
    
    protected void Crouch(InputAction.CallbackContext context)
    {
        _isDownPressed = true;
    }
    
    protected void CrouchCanceled(InputAction.CallbackContext context)
    {
        _isDownPressed = false;
    }
    
    protected void JumpCanceled(InputAction.CallbackContext context)
    {
        _desiredJump = false;
    }

    protected void PassThroughPlatform(InputAction.CallbackContext context)
    {
        _ground.PassCurPlatform(); //todo: put in move instead of move1
    }
    
}

    
    