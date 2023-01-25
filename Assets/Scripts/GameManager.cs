using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] private AudioSource oneTwoThreeGo;
    private int playerIdx = 0;
    private InputManager _inputManager;
    private PlayerControls _controls;


    // Start is called before the first frame update
    void Start()
    {
        _inputManager = GameObject.Find("InputManager(Clone)").GetComponent<InputManager>();
        oneTwoThreeGo.Play();
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
        endScreen.GetComponent<endScreen>().isActive = true;

    }

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.UI.Enter.Enable();
        _controls.UI.Enter.performed += EnterFunc;
        _controls.UI.Escape.Enable();
        _controls.UI.Escape.performed += EscFunc;;
    }
    
    private void OnDisable()
    {
        _controls.UI.Enter.Disable();
        _controls.UI.Escape.Disable();
    }

    private void AddPlayersToListNew()
    {

        List<PlayerInput> playerInputs = _inputManager.playerInputs;
        int numPlayersToAdd = playerInputs.Count;
        print(numPlayersToAdd);
        playerList = new List<Player>();
        // List<bool> playerBoolList = ChoosePlayerManager.playersArr;
        
        if (numPlayersToAdd >= 1)
        {
            playerList.Add(Player1);
            Player1.MoveScript = _inputManager.playerInputs[0].GetComponent<MoveScript>();
            Player1.MoveScript.isPlayScene = true;
        }
        if (numPlayersToAdd >= 2)
        {
            playerList.Add(Player2);
            Player2.MoveScript = _inputManager.playerInputs[1].GetComponent<MoveScript>();
            Player2.MoveScript.isPlayScene = true;
        }
        if (numPlayersToAdd >= 3)
        {
            playerList.Add(Player3);
            Player3.MoveScript = _inputManager.playerInputs[2].GetComponent<MoveScript>();
            Player3.MoveScript.isPlayScene = true;
        }
        if (numPlayersToAdd >= 4)
        {
            playerList.Add(Player4);
            Player4.MoveScript = _inputManager.playerInputs[3].GetComponent<MoveScript>();
            Player4.MoveScript.isPlayScene = true;
        }
        foreach (Player player in playerList)
        {
            player.gameObject.SetActive(true);
        }
    }

    public void CountDownFinish()
    {
        countDownFinish = true;
        GetComponent<AudioSource>().Play();
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
    // private void addPlayersToList()
    // {
    //     playerList = new List<Player>();
    //     
    //     switch (numberOfPlayers)
    //     {
    //         case 4:
    //             playerList.Add(Player4);
    //             playerList.Add(Player3);
    //             playerList.Add(Player2);
    //             playerList.Add(Player1);
    //             break;
    //         case 3:
    //             playerList.Add(Player3);
    //             playerList.Add(Player2);
    //             playerList.Add(Player1);
    //             break;
    //         case 2:
    //             playerList.Add(Player2);
    //             playerList.Add(Player1);
    //             break;
    //         case 1:
    //             playerList.Add(Player1);
    //             break;
    //     }
    //
    //     foreach (Player player in playerList)
    //     {
    //         player.gameObject.SetActive(true);
    //     }
    // }
    
    
    
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
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Pause();
        }
        List<Player> rankList = new List<Player>();
        if (playersAlive == -1)
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
        fallenPlayers.Insert(0, player);
        player.gameObject.SetActive(false);
        playersAlive --;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList[playerIdx].MoveScript = playerInput.GetComponent<MoveScript>();
        playerIdx++;
    }
    
    private void EnterFunc(InputAction.CallbackContext context)
    {
    }
    
    private void EscFunc(InputAction.CallbackContext context)
    {
    }

    public void BackToMainMenu(GameObject endScene)
    {
        Destroy(endScene);
        SceneManager.LoadScene("OpeningScene");
    }

}
