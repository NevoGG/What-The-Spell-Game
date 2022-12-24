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
    
    //Defaults:
    private static int defNumOfPlayers = 4;

    //Offline Game Parameters:
    [SerializeField] private Witch Witch1;
    [SerializeField] private Witch Witch2;
    [SerializeField] private Player Player1;
    [SerializeField] private Player Player2;
    [SerializeField] private Player Player3;
    [SerializeField] private Player Player4;
    
    private int numberOfPlayers;
    private int playersAlive;
    private bool gameEnded;
    private List<Player> playerList;
    

    // Start is called before the first frame update
    void Start()
    {
        // numberOfPlayers = WriteNumPlayers.numberOfPlayers;
        gameEnded = false;
        numberOfPlayers = 1;
        playersAlive = numberOfPlayers;
        addPlayersToList();
    }

    /**
     * Checks amount of players as init and adds to a list the wanted amount, also activates there gameobjects
     */
    private void addPlayersToList()
    {
        playerList = new List<Player>(4);
        
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
        if (!gameEnded)
        {
            
        }
        UpdatePlayersAlive();
        gameEnded = Timer.timerDone || playersAlive == 0 || playersAlive == 1;
        if (gameEnded)
        {
            endGame();
        }
    }

    private void UpdatePlayersAlive()
    {
        int count = 0;
        foreach (var player in playerList)
        {
            if (player.gameObject.activeSelf)
            {
                count++;
            }
        }
        playersAlive = count;
    }
    private void endGame()
    {
        if (playersAlive == 0)
        {
            Tie();
        }
        else
        {
            List<Player> winnerList = GetWinner();
        }
        
    }

    private void Tie()
    {
        print("Its a tie");
    }


    /**
     * Gets a list of all the alive players with the max score.
     */
    private List<Player> GetWinner()
    {
        List<Player> winnerList = new List<Player>();
        int max = 0;
        foreach (var player in playerList)
        {
            if (player.gameObject.activeSelf)
            {
                if (player.getScore() > max)
                {
                    max = player.getScore();
                }
            }
        }
    
        foreach (var player in playerList)
        {
            if (player.gameObject.activeSelf)
            {
                if (max == player.getScore())
                {
                    winnerList.Add(player);
                }
            }
        }
        return winnerList;
    }
}
