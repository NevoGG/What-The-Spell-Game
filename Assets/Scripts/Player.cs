using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ScoreEnum {XP, GrowSpells, ShrinkSpells, BiggestAnimal, TimeAsBiggest }

public class Player : MonoBehaviour
{
	[SerializeField] private List<GameObject> animals;
	[SerializeField] private Arrow arrow;
	public bool hasChanged = false;
	//Other Parameters
	private int _curAnimalXPNeeded;
	public GameObject curAnimal;
	private int curAnimalIdx = 0;

	private AnimalPower _animalPower;
	[SerializeField] public GameManager _gameManager;

	//Online Parameters:
	private int curXpFromLastLevel = 0;
	private bool _hasChanged = false;
	//Score:
	public int xp = 0;
	public int growSpells = 0;
	public int shrinkSpells = 0;
	public int biggestAnimal = 0;

	void awake()
	{
	}

	// Start is called before the first frame update
	void Start()
	{
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
	    if (!this._hasChanged)
	    {
		    // SpellCasted(SpellEnum.Grow);
		    // SpellCasted(SpellEnum.Grow);
		    // SpellCasted(SpellEnum.Grow);
		    _hasChanged = true;
	    }
    }

	void FixedUpdate()
	{
	
	}

	public void MoveToPosition(Vector3 pos)
	{
		animals[curAnimalIdx].transform.position = pos;
	}
	
	public void SpellCasted(SpellEnum spell)
	{
		hasChanged = true;
		switch (spell)
	    {
		    case SpellEnum.Shrink:
			    Shrink();
		     curXpFromLastLevel -= 1;
		     if (xp > 0)
		     {
			     // curAnimal.GetComponent<Animal>().shrinkParticles.Play();
			     xp -= 1;
		     }
		     shrinkSpells += 1;
		     if (curXpFromLastLevel < 0) Demote();
		     break;
	     case SpellEnum.Grow:
		     Grow();
		     xp += 1;
		     // curAnimal.GetComponent<Animal>().growParticles.Play();
		     growSpells += 1;
			curXpFromLastLevel += 1;
			if (curXpFromLastLevel == _curAnimalXPNeeded) Promote();
			break;
	     default: return;
	    }
	    //Scalable.
    } 

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
		    curXpFromLastLevel = _curAnimalXPNeeded - 1;
	    }else
	    {
		    curAnimalIdx += 1;
		    if (curAnimalIdx > biggestAnimal) biggestAnimal = curAnimalIdx;
		    UpdateAnimal();
	    }
    }


    private void UpdateAnimal() //Update animal to current index, update fields.
    {
	    Vector3 velocity = curAnimal.GetComponent<Rigidbody2D>().velocity;
	    transform.position = curAnimal.transform.position;
	    curAnimal.SetActive(false);
		curAnimal = animals[curAnimalIdx];
		curAnimal.transform.position = transform.position;
		curAnimal.SetActive(true);
		arrow.SetFollowing(curAnimal);
		curAnimal.GetComponent<Rigidbody2D>().velocity = velocity;
		GetAnimalPhysics();
    }
    

    private void GetAnimalPhysics()
    {
	    Animal curAnimObj = curAnimal.GetComponent<Animal>();
	    //General Parameters:
	    _animalPower = curAnimObj.GetAnimalPow();
	    _curAnimalXPNeeded = curAnimObj.GetXpNeeded();
    }
    //returns cur animal, total xp needed for next, xp left. animal index starts from 0
    public Tuple<int, int, int> GetScore() //returns cur animal, total xp needed for next, xp left.
    {
	    return new Tuple<int, int, int>(curAnimalIdx, _curAnimalXPNeeded, curXpFromLastLevel);
    }

    public void  HasLost()
    {
	    
	    // _gameManager.GetComponent<GameManager>().PlayerLost(this);
	    _gameManager.PlayerLost(this);
    }

    public void TurnToBlob()
    {
	    return;//todo: later.
    }

    public void OperateEndGame(float size)
    {
	    curAnimal.transform.localScale *= size;
    }

    private void Grow()
    {
	    //todo: Fede 
    }

    private void Shrink()
    {
	    //todo: Fede 
    }

    public void Dash()
    {
	    //todo: Fede 
    }

    public void Jump()
    {
	    //todo: Fede 
    }
    
} //Upgrade the animal of the player


