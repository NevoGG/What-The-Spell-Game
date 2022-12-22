using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
    {
        [SerializeField] private InputController input = null;
        private float _jumpHeight = 3f;
        private int _maxAirJumps = 0;
        private float _downwardMovementMultiplier = 3f;
        private float _upwardMovementMultiplier = 1.7f;

        // private Controller _controller;
        private Rigidbody2D _body;
        private Ground _ground;
        private Vector2 _velocity;

        private int _jumpPhase;
        private float _defaultGravityScale, _jumpSpeed;

        private bool _desiredJump, _onGround;
        
        
        //Setters:
        public void SetInputController(InputController ic) { input = ic;}
        public void SetJumpHeight(float h) { _jumpHeight = h;}
        public void SetMaxAirJumps(int h) { _maxAirJumps = h;}
        public void SetDownwardMovementMultiplier(float h) { _downwardMovementMultiplier = h;}
        public void SetUpwardMovementMultiplier(float h) { _upwardMovementMultiplier = h;}
        public void SetRigidBody(Rigidbody2D rb) { _body = rb;}
        
        // Start is called before the first frame update
        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
            // _controller_controller = GetComponent<Controller>();

            _defaultGravityScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            _desiredJump |= input.RetrieveJumpInput();
        }

        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _velocity = _body.velocity;

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
    }
