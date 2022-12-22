using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //Movement Fields:
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
    
    //Jump Fields:
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    
    //Other Fields:
    [SerializeField, Range(0f, 100f)] private float _size = 10f;
    [SerializeField] private AnimalPower _power;
    [SerializeField, Range(0f, 10)] private int _xpNeeded;
    
    private Player player;
    
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
        Vector3 curSize = transform.localScale;
        transform.localScale = new Vector3(curSize.x * _size, curSize.y * _size, curSize.z * _size);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hi");
        SpellEnum spell = SpellEnum.None;
        Debug.Log(collision.gameObject.tag);
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
}

//todo: scaling to center and keep ground level the same.