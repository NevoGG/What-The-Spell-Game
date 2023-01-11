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
    public abstract void SetMaxAirJumps(int k);

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
    }
    
    protected void CreateDust()
    {
        dust.Play();
    }
}

    
    