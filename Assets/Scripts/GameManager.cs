using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public const string FALL_BOUNDER_TAG = "fallBounder";
    
    
    
    //Statics:
    public static bool gameEnded;
    public static bool countDownFinish;

    //Offline Game Parameters:
    [SerializeField] private Witch Witch1;
    [SerializeField] private Witch Witch2;
    [SerializeField] private Player Player1;
    [SerializeField] private Player Player2;
    [SerializeField] private Player Player3;
    [SerializeField] private Player Player4;
    [SerializeField] private GameObject endScreen;

    public int numberOfPlayers;
    [SerializeField]private int playersAlive;
    private List<Player> playerList;
    private List<Player> fallenPlayers;
    private bool firstGameEnded;
    private bool PlayerCheckGame; // if we chose 1 player at beginning to check game
    // [SerializeField] private AudioSource oneTwoThreeGo;
    private GameObject sFX;
    // Start is called before the first frame update
    void Start()
    {
        sFX = GameObject.FindGameObjectWithTag("SFX");
        sFX.GetComponent<SFXManager>().playOneTwoThreeGo();
        sFX.GetComponent<SFXManager>().pauseMenuMusic();
        // oneTwoThreeGo.Play();
        fallenPlayers = new List<Player>();
        //numberOfPlayers = WriteNumPlayers.numberOfPlayers;
        numberOfPlayers = ChoosePlayerManager.numberOfPlayers;
        gameEnded = false;
        playersAlive = numberOfPlayers;
        //addPlayersToList();
        AddPlayersToListNew();
        countDownFinish = false;
        SetGameManagerInPlayers();
        firstGameEnded = false;
        PlayerCheckGame = numberOfPlayers == 1;
    }

    private void AddPlayersToListNew()
    {
        playerList = new List<Player>();
        List<bool> playerBoolList = ChoosePlayerManager.playersArr;

        if (playerBoolList[0])
        {
            playerList.Add(Player1);
            Player1.GetArrow().SetArrowActive(true);
        }
        if (playerBoolList[1])
        {
            playerList.Add(Player2);
            Player2.GetArrow().SetArrowActive(true);
        }
        if (playerBoolList[2])
        {
            playerList.Add(Player3);
            Player3.GetArrow().SetArrowActive(true);
        }
        if (playerBoolList[3])
        {
            playerList.Add(Player4);
            Player4.GetArrow().SetArrowActive(true);
        }
        foreach (Player player in playerList)
        {
            player.gameObject.SetActive(true);
        }
    }

    public void CountDownFinish()
    {
        countDownFinish = true;
        // GetComponent<AudioSource>().Play();
        // sFX.GetComponent<SFXManager>().playMenuMusic();
        sFX.GetComponent<SFXManager>().playSonicMusic();
    }
    

    private void SetGameManagerInPlayers()
    {
        foreach (var player in playerList)
        {
            player._gameManager = this;
        }
    }



    // Update is called once per frame
    void Update()
    {
        // UpdatePlayersAlive();
        gameEnded = Timer.timerDone || playersAlive == 0 || (playersAlive == 1 && !PlayerCheckGame);
        if (gameEnded && !firstGameEnded)
        {
            firstGameEnded = true;
            endGame();
        }
    }
    
    private void endGame()
    {
        // if (GetComponent<AudioSource>().isPlaying)
        // {
        //     GetComponent<AudioSource>().Pause();
        // }
        sFX.GetComponent<SFXManager>().stopSonicMusic();
        List<Player> rankList = new List<Player>();
        if (playersAlive == -1)
        {
            //tie: nothing at the moment
        }
        else
        {
            GetWinner(rankList);
        }
        endScreen.SetActive(true);
        Witch1.gameObject.SetActive(false);
        Witch2.gameObject.SetActive(false);
        endScreen.GetComponent<endScreen>().EndGame(rankList);
    }


    /**
     * Gets a list of all the alive players with the max score.
     */
    private List<Player> GetWinner(List<Player> rankList)
    {
        foreach (var player in playerList)
        {
            if (player.gameObject.activeSelf)
            {
                rankList.Add(player);
            }
        }
        rankList.Sort(delegate(Player player1, Player player2)
        {
            if (player1.xp > player2.xp) return -1;
            if (player1.xp < player2.xp) return 1;
            if (player1.xp == player2.xp)
            {
                if (player1.growSpells > player2.growSpells) return -1;
                if (player1.growSpells < player2.growSpells) return 1;
            }
            return 0;
        });
        foreach (var player in fallenPlayers)
        {
            rankList.Add(player);
        }
        return rankList;
    }

    public void PlayerLost(Player player)
    {
        player.GetArrow().ArrowDestroy();
        fallenPlayers.Insert(0, player);
        player.gameObject.SetActive(false);
        playersAlive --;
    }
}
