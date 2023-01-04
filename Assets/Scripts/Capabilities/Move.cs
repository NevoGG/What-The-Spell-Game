using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public abstract class Move : MonoBehaviour
{
    protected PlayerInput1 controls; 
    protected InputAction move;
    protected InputAction jump;
    protected InputAction crouch;
    protected InputAction power;
    //animation parameters: 
    protected bool isFacingRight = true;
    
    //Movement Fields:
    [SerializeField, Range(0f, 1000f)] protected float _maxSpeed = 15f;
    [SerializeField, Range(0f, 300f)] protected float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 1000f)] protected float _maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 10)] protected float _downwardOnPressMultiplier = 2f;	
    
    //Jump Fields:
    [SerializeField, Range(0f, 100f)] protected float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 100f)] protected float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 1000f)] protected float _jumpHeight = 100f;
    [SerializeField, Range(0f, 1f)] protected float _multiJumpMultiplier = 0.75f;
    
    //linear drag:
    [SerializeField, Range(0f, 10f)] protected float  _groundLinearDrag= 10f;
    [SerializeField, Range(0f, 10f)] protected float  _upwardLinearDrag= 10f;
    [SerializeField, Range(0f, 10f)] protected float  _downardLinearDrag= 10f;
    [SerializeField, Range(0f, 15f)] protected float _size = 1f;
    
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
    public abstract void SetMaxAirJumps(int k);
    
}

    
    