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

    private int numberOfPlayers;
    [SerializeField]private int playersAlive;
    private List<Player> playerList;
    private List<Player> fallenPlayers;
    private bool firstGameEnded;
    private bool PlayerCheckGame; // if we chose 1 player at beginning to check game
    



    // Start is called before the first frame update
    void Start()
    {
        fallenPlayers = new List<Player>();
        numberOfPlayers = WriteNumPlayers.numberOfPlayers;
        gameEnded = false;
        playersAlive = numberOfPlayers;
        addPlayersToList();
        countDownFinish = false;
        SetGameManagerInPlayers();
        firstGameEnded = false;
        PlayerCheckGame = numberOfPlayers == 1;
    }

    public void SetCountDownFinish()
    {
        countDownFinish = true;
    }

    private void SetGameManagerInPlayers()
    {
        foreach (var player in playerList)
        {
            player._gameManager = this;
        }
    }

    /**
     * Checks amount of players as init and adds to a list the wanted amount, also activates there gameobjects
     */
    private void addPlayersToList()
    {
        playerList = new List<Player>();
        
        switch (numberOfPlayers)
        {
            case 4:
                playerList.Add(Player4);
                playerList.Add(Player3);
                playerList.Add(Player2);
                playerList.Add(Player1);
                break;
            case 3:
                playerList.Add(Player3);
                playerList.Add(Player2);
                playerList.Add(Player1);
                break;
            case 2:
                playerList.Add(Player2);
                playerList.Add(Player1);
                break;
            case 1:
                playerList.Add(Player1);
                break;
        }

        foreach (Player player in playerList)
        {
            player.gameObject.SetActive(true);
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

    // private void UpdatePlayersAlive()
    // {
    //     int count = 0;
    //     foreach (var player in playerList)
    //     {
    //         if (player.gameObject.activeSelf)
    //         {
    //             count++;
    //         }
    //     }
    //     playersAlive = count;
    // }
    private void endGame()
    {
        List<Player> rankList = new List<Player>();
        if (playersAlive == 0)
        {
            Tie();
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

    private void Tie()
    {
        // print("Its a tie");
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
            if (player1.xp > player2.xp) return 1;
            if (player1.xp < player2.xp) return -1;
            if (player1.xp == player2.xp)
            {
                if (player1.growSpells > player2.growSpells) return 1;
                if (player1.growSpells < player2.growSpells) return -1;
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
        fallenPlayers.Insert(0, player);
        player.gameObject.SetActive(false);
        playersAlive --;
    }


}
