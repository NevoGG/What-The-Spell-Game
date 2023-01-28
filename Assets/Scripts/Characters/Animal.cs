using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //Other Fields:

    [SerializeField] private AnimalPower _power = AnimalPower.DoubleJump;
     private int _xpNeeded = 1;
    [SerializeField] public ParticleSystem growParticles;
    [SerializeField] private ParticleSystem shrinkParticles; 
    
    //dash pushed factor
    private float _yPushVelocity= 40f;
    private float _pushFactor = 100f;

    private Move _move;
    private Ground _ground;
    private Rigidbody2D _body;
    public Player player;
    private BoxCollider2D _boxCollider;

//small change here
    
    //Setters:
    public void SetPlayer(Player p)
    {
        player = p;
        GetComponent<Move>().SetPlayer(p);
    }
    //Getters:
    public AnimalPower GetAnimalPow() {return _power;}
    public int GetXpNeeded() {return _xpNeeded;}

    public Player GetPlayer()
    { return player;}
    
    void Start()
    {
        _move = GetComponent<Move>();
        _ground = GetComponent<Ground>();
        _body = GetComponent<Rigidbody2D>();
		SetAnimalPower();
    }
    

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
                collision.gameObject.SetActive(false);
                break;
            case  GameManager.GROWSPELL:
                spell = SpellEnum.Grow;
                collision.gameObject.SetActive(false);
                break;
            case GameManager.PLAYER_TAG:
                if (CompareTag("Dashing"))
                {
                    growParticles.Play();
                    PushPlayer(collision);
                } 
                break;
            default: return;
        }
        player.SpellCasted(spell);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        
        SpellEnum spell = SpellEnum.None;
        switch (collision.gameObject.tag)
        {
            case GameManager.SHRINKSPELL:
                spell = SpellEnum.Shrink;
                collision.gameObject.SetActive(false);
                break;
            case  GameManager.GROWSPELL:
                spell = SpellEnum.Grow;
                collision.gameObject.SetActive(false);
                break;
            default: return;
        }
        player.SpellCasted(spell);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SpellEnum spell = SpellEnum.None;
        switch (collision.gameObject.tag)
        {
            case GameManager.FALL_BOUNDER_TAG:
                HasLost();
                break;
            default: return;
        }
        player.SpellCasted(spell);
    }
    
    private void SetAnimalPower()
    {
        switch (_power)
        {
            case AnimalPower.DoubleJump:
                _move.SetMaxAirJumps(1);
                break;
            default:
                _move.SetMaxAirJumps(0);
                break;
        }
    }
    
    private void PushPlayer(Collision2D collision)
    {
        float otherVelocityX = _body.velocity.x;
        int pushDir = otherVelocityX > 0 ? 1 : -1;
        _body.velocity = new Vector2(pushDir * _pushFactor, _yPushVelocity);
    }

    public void HasLost()
    {
        player.HasLost();
    }
}
