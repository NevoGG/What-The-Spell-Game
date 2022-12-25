using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //Movement Fields:
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 15f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    
    //Jump Fields:
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 10f;
    
    //Other Fields:
    [SerializeField, Range(0f, 100f)] private float _size = 10f;
    [SerializeField] private AnimalPower _power;
    [SerializeField, Range(0f, 10)] private int _xpNeeded;
    
    	
    private Move _move;
    private Ground _ground;
    private Rigidbody2D _body;
    private Player player;
    private BoxCollider2D _boxCollider;

    
    //Setters:
    public void SetPlayer(Player p) { player = p;}
    //Getters:
    public float GetMaxSpeed (){return _maxSpeed;}
    public float GetMaxAcceleration (){return _maxAcceleration;}
    public float GetMaxAirAcceleration (){return _maxAirAcceleration;}
    public float GetDownwardMovementMultiplier (){return _downwardMovementMultiplier;}
    public float GetUpwardMovementMultiplier (){return _upwardMovementMultiplier;}
    public float GetJumpHeight (){return _jumpHeight;}
    public AnimalPower GetAnimalPow() {return _power;}
    public int GetXpNeeded() {return _xpNeeded;}

    void Start()
    {
        //Scale to size:
        _boxCollider = GetComponent<BoxCollider2D>();
        transform.localScale = transform.localScale * _size;
        _boxCollider.size = Vector2.one * 0.8f;
        

        _move = GetComponent<Move>();
        _ground = GetComponent<Ground>();
        _body = GetComponent<Rigidbody2D>();
        UpdateJumpParameters();
        UpdateMoveParameters();

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpellEnum spell = SpellEnum.None;
        switch (collision.gameObject.tag)
        {
            case GameManager.SHRINKSPELL:
                spell = SpellEnum.Shrink;
                break;
            case  GameManager.GROWSPELL:
                spell = SpellEnum.Grow;
                break;
            //Scalable. todo: more spells?
            default: return;
        }
        player.SpellCasted(spell);
    }
    
    private void UpdateJumpParameters()
    {
        int _maxAirJumps = 0;
        if(_power == AnimalPower.DoubleJump) _maxAirJumps = 1;
        _move.SetJumpHeight(_jumpHeight);
        _move.SetMaxAirJumps(_maxAirJumps);
        _move.SetDownwardMovementMultiplier(_downwardMovementMultiplier);
        _move.SetUpwardMovementMultiplier(_upwardMovementMultiplier);
    }

    private void UpdateMoveParameters()
    {
        _move.SetMaxSpeed(_maxSpeed);
        _move.SetMaxAcceleration(_maxAcceleration);
        _move.SetMaxAirAcceleration(_maxAirAcceleration);
    }
}

//todo: scaling to center and keep ground level the same.