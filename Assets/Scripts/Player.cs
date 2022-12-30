using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreEnum {XP, GrowSpells, ShrinkSpells, BiggestAnimal, TimeAsBiggest }

public class Player : MonoBehaviour
{
	[SerializeField] private List<GameObject> animals;

	//Other Parameters
	private int _curAnimalXPNeeded;
	private GameObject curAnimal;
	private int curAnimalIdx = 0;

	private AnimalPower _animalPower;
	private GameManager _gameManager;

	//Online Parameters:
	private int curXpFromLastLevel = 0;

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
    }

	void FixedUpdate()
	{
	
	}

	public void SetGameManager(GameManager gm) { _gameManager = gm;} //todo: use this from gameManager
	
	public void SpellCasted(SpellEnum spell)
	{
		switch (spell)
	    {
	     case SpellEnum.Shrink:
		     curXpFromLastLevel -= 1;
		     if (xp > 0) xp -= 1;
		     shrinkSpells += 1;
		     if (curXpFromLastLevel < 0) Demote();
		     break;
	     case SpellEnum.Grow:
		     xp += 1;
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
	    transform.position = curAnimal.transform.position;
	    curAnimal.SetActive(false);
		curAnimal = animals[curAnimalIdx];
		curAnimal.transform.position = transform.position;
		curAnimal.SetActive(true);
		GetAnimalPhysics();
    }
    

    private void GetAnimalPhysics()
    {
	    Animal curAnimObj = curAnimal.GetComponent<Animal>();
	    //General Parameters:
	    _animalPower = curAnimObj.GetAnimalPow();
	    _curAnimalXPNeeded = curAnimObj.GetXpNeeded();
    }
    
 //   public void getScore()
   // {
	//    int score;
	//    foreach (var animal in animals)
	//    {
	//	    
	//    }
   // }

    public void  HasLost()
    {
	    // _gameManager.PlayerLost(this); todo:function doesnt exist yet.
    }

    public int getScore()
    {
	    return 0;
    } // todo: delete later.


} //Upgrade the animal of the player


