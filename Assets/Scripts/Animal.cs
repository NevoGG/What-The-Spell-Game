using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //Other Fields:

    [SerializeField] private AnimalPower _power = AnimalPower.DoubleJump;
    [SerializeField, Range(0f, 10)] private int _xpNeeded = 2;
    
    // public ParticleSystem growParticles;
    // public ParticleSystem shrinkParticles;
    private Move _move;
    private Ground _ground;
    private Rigidbody2D _body;
    private Player player;
    private BoxCollider2D _boxCollider;

//small change here
    
    //Setters:
    public void SetPlayer(Player p) { player = p;}
    //Getters:
    public AnimalPower GetAnimalPow() {return _power;}
    public int GetXpNeeded() {return _xpNeeded;}

    void Start()
    {
        _move = GetComponent<Move>();
        _ground = GetComponent<Ground>();
        _body = GetComponent<Rigidbody2D>();
		SetAnimalPower();
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
                collision.gameObject.SetActive(false); //todo: spell set inactive
                break;
            case  GameManager.GROWSPELL:
                spell = SpellEnum.Grow;
                collision.gameObject.SetActive(false);
                break;
            case GameManager.FALL_BOUNDER_TAG:
                player.HasLost();
                break;
            //Scalable. todo: more spells?
            default: return;
        }
        player.SpellCasted(spell);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SpellEnum spell = SpellEnum.None;
        switch (collision.gameObject.tag)
        {
            case GameManager.SHRINKSPELL:
                spell = SpellEnum.Shrink;
                collision.gameObject.SetActive(false); //todo: spell set inactive
                break;
            case  GameManager.GROWSPELL:
                spell = SpellEnum.Grow;
                collision.gameObject.SetActive(false);
                break;
            case GameManager.FALL_BOUNDER_TAG:
                player.HasLost();
                break;
            default: return;
        }
        player.SpellCasted(spell);
    }
    
    private void SetAnimalPower()
    {
        switch (_power)
        { //todo: set later to 1 and 0
            case AnimalPower.DoubleJump:
                _move.SetMaxAirJumps(1);
                break;
            default:
                _move.SetMaxAirJumps(0);
                break;
        }
    }
    
}

//todo: scaling to center and keep ground level the same.