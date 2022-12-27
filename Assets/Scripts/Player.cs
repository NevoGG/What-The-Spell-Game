using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
	[SerializeField] private List<GameObject> animals = new List<GameObject>();
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


	void awake()
	{
	}

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Started");

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
		Debug.Log("Animal: ");
		Debug.Log(curAnimalIdx);
		Debug.Log("XP level: ");
		Debug.Log(curXpFromLastLevel);
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
	    curXpFromLastLevel = 0;
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
	    transform.position = curAnimal.transform.position;
	    curAnimal.SetActive(false);
		curAnimal = animals[curAnimalIdx];
		curAnimal.transform.position = transform.position;
		curAnimal.SetActive(true);
		UpdateAnimalTraits();
    }

    private void UpdateAnimalTraits()
    {
	    GetAnimalPhysics();
    }

    private void GetAnimalPhysics()
    {
	    Animal curAnimObj = curAnimal.GetComponent<Animal>();
	    //General Parameters:
	    _animalPower = curAnimObj.GetAnimalPow();
	    _curAnimalXPNeeded = curAnimObj.GetXpNeeded();
    }

    public int getScore() //todo: erase
    {
        return 0;
    }
    
    
} //Upgrade the animal of the player


