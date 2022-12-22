using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
	[SerializeField] private List<GameObject> animals;
	//Controller Parameters:
	[SerializeField] private InputController input = null;
	
    //Movement Parameters:
    private float _maxSpeed = 4f;
    private float _maxAcceleration = 35f;
    private float _maxAirAcceleration = 20f;
    
    //Jump Parameters:
    private float _jumpHeight = 3f;
    private int _maxAirJumps = 0;
    private float _downwardMovementMultiplier = 3f;
    private float _upwardMovementMultiplier = 1.7f;

    //Other Parameters
    private int _curAnimalXPNeeded;
    private GameObject curAnimal;
	private int curAnimalIdx = 0;
	private AnimalPower _animalPower;
	
	//Online Parameters:
	private int curXpFromLastLevel = 0;
	
	private Move _move;
	private Jump _jump;
	private Ground _ground;
	private Rigidbody2D _body;

	void awake()
	{
	}

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Started");
		_move = GetComponent<Move>();
		_jump = GetComponent<Jump>();
		_ground = GetComponent<Ground>();
		_body = GetComponent<Rigidbody2D>();
		foreach(GameObject obj in animals)
		{
			Animal anim = obj.GetComponent<Animal>();
			anim.SetPlayer(this);
			obj.SetActive(false);
		}
		curAnimal = animals[curAnimalIdx];
		UpdateAnimal();
	}
	// Update is called once per frame
	void Update()
	{
        
	}


    public void SpellCasted(SpellEnum spell)
    {
	    switch (spell)
	    {
	     case SpellEnum.Shrink:
		     curXpFromLastLevel -= 1;
		     if (curXpFromLastLevel < 0) Demote();
		     break;
	     case SpellEnum.Grow:
			curXpFromLastLevel += 1;
			if (curXpFromLastLevel == _curAnimalXPNeeded) Promote();
			break;
	     default: return;
	    }
	    //Scalable.
    } //Alerted that a spell hit the player

    private void Demote() //Downgrade the animal of the player
    {
	    curXpFromLastLevel = 0;
	    if (curAnimalIdx == 0) return; 
	    else
	    {
		    curAnimalIdx -= 1;
		    UpdateAnimal();
	    }
    }

    private void Promote()
    {
	    if (curAnimalIdx == animals.Count - 1)
	    {
		    //todo: what happend if I'm at the highest one and want to grow?
		    curXpFromLastLevel = _curAnimalXPNeeded - 1;
		    return;
	    }
	    else
	    {
		    curAnimalIdx += 1;
		    UpdateAnimal();
	    }
    }
    
    private void UpdateAnimal() //Update animal to current index, update fields.
    {
	    curAnimal.SetActive(false);
		curAnimal = animals[curAnimalIdx];
		curAnimal.SetActive(true);
		UpdateAnimalTraits();
    }

    private void UpdateAnimalTraits()
    {
	    GetAnimalPhysics();
	    UpdateJumpParameters();
	    UpdateMoveParameters(); 
    }

    private void GetAnimalPhysics()
    {
	    Animal curAnimObj = curAnimal.GetComponent<Animal>();
	    _body = curAnimal.GetComponent<Rigidbody2D>();
	    //General Parameters:
	    _animalPower = curAnimObj.GetAnimalPow();
	    _curAnimalXPNeeded = curAnimObj.GetXpNeeded();
	    
	    //Move Parameters:
	    _maxSpeed = curAnimObj.GetMaxSpeed();
		_maxAcceleration = curAnimObj.GetMaxAcceleration();
		_maxAirAcceleration = curAnimObj.GetMaxAirAcceleration();
    
		//Jump Parameters:
		_jumpHeight = curAnimObj.GetJumpHeight ();
		_downwardMovementMultiplier = curAnimObj.GetDownwardMovementMultiplier ();
		_upwardMovementMultiplier =  curAnimObj.GetUpwardMovementMultiplier ();
		_maxAirJumps = 0;
		if(_animalPower == AnimalPower.DoubleJump) _maxAirJumps = 1;
    }

    private void UpdateJumpParameters()
    {
	    _jump.SetJumpHeight(_jumpHeight);
	    _jump.SetMaxAirJumps(_maxAirJumps);
	    _jump.SetDownwardMovementMultiplier(_downwardMovementMultiplier);
	    _jump.SetUpwardMovementMultiplier(_upwardMovementMultiplier);
	    _jump.SetRigidBody(_body);
    }

    private void UpdateMoveParameters()
    {
	    _move.SetMaxSpeed(_maxSpeed);
	    _move.SetMaxAcceleration(_maxAcceleration);
	    _move.SetMaxAirAcceleration(_maxAirAcceleration);
	    _move.SetRigidBody(_body);
	    //todo: fill 
    }
    
    
} //Upgrade the animal of the player


