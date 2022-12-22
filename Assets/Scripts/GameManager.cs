using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Platform {}
public enum PlayerEnum { Player1, Player2, Player3, Player4, None}
public enum SpellEnum { Grow, Shrink, None}
public enum AnimalEnum { Animal1, Animal2, Animal3, Animal4}
public enum AnimalPower { DoubleJump, AnimalPow2, AnimalPow3, AnimalPow4 }

public class GameManager : MonoBehaviour
{

    //tags:
    public const string PLATFORM_TAG = "Platform";
    public const string SHRINKSPELL = "Shrink";
    public const string GROWSPELL = "Grow";
    public const string PLAYER_TAG = "Player";
    public const string TERRAIN_TAG = "Terrain";
    public const string POWER_TAG = "Power"; //todo: maybe for each power
    
    //Defaults:
    private static int defWitchMovementSpeed = 5;
    private static int defWitchShotPerMinute =10; 
    private static float defWitchGrowToShrinkRatio = 0.5f; 
    private static int defNumOfPlayers = 4;
    private static int defTimerLenPerRound = 60;
         
    //Offline Game Parameters:
    [SerializeField] private int witchMovementSpeed = defWitchMovementSpeed;
    [SerializeField] private int witchShotPerMinute = defWitchShotPerMinute;
    [SerializeField] private float witchGrowToShrinkRatio = defWitchGrowToShrinkRatio;
    [SerializeField] private float timerLenPerRound = defTimerLenPerRound;
    [SerializeField] private Witch Witch1;
    [SerializeField] private Witch Witch2;
    [SerializeField] private Player Player1;
    [SerializeField] private Player Player2;
    [SerializeField] private Player Player3;
    [SerializeField] private Player Player4;
    
    //Online Game Parameters:
    private float curTimer;
    
    
    //signatures to clarify GameManager responsibility
    private void StartTimer() {}
    private void IsTimerOver() {}
    private Player GetWinner() { return new Player();
    } //return enum of player who won, if none, return Player.None
    
    public void UpdateDeadPlayer(Player player)
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //set witchInitialParameters
        
    }
    
    private void SetWitchInitParams(Witch witch)
    {
        witch.SetWitchMovementSpeed(witchMovementSpeed);
        witch.SetWitchShotPerMinute(witchShotPerMinute);
        witch.SetWitchGrowToShrinkRatio(witchGrowToShrinkRatio);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
