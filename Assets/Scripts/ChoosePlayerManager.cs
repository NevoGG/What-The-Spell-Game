using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    // public static List<bool> playersArr = new List<bool>{false, false, false, false};
    public static int numberOfPlayers = 0;
    

    [SerializeField] private Image enterRenderer;
    [SerializeField] private Sprite enterClicked;
    [SerializeField] private Image redKeysRenderer;
    [SerializeField] private Image blueKeysRenderer;
    [SerializeField] private Image greenKeysRenderer;
    [SerializeField] private Image yellowKeysRenderer;
    [SerializeField] private Sprite greyBlueKeys;
    [SerializeField] private Sprite greyYellowKeys;
    [SerializeField] private Sprite greyRedKeys;
    [SerializeField] private Sprite greyGreenKeys;
    [SerializeField] private Sprite blueKeys;
    [SerializeField] private Sprite yellowKeys;
    [SerializeField] private Sprite redKeys;
    [SerializeField] private Sprite greenKeys;
    
    [SerializeField] private AudioSource playerConnect1;
    [SerializeField] private AudioSource playerConnect2;
    [SerializeField] private AudioSource playerConnect3;
    [SerializeField] private AudioSource playerConnect4;
    [SerializeField] private AudioSource buttonPress;
    
    private PlayerControls _controls;
    // Start is called before the first frame update
    void Awake()
    {
        _inputManager = GameObject.Find("InputManager(Clone)").GetComponent<InputManager>();
        // ResetPlayers();
        _controls = new PlayerControls();
    }

    private void Start()
    {
        ResetPlayers();
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

    // Update is called once per frame
    void Update()
    {
        if (_inputManager.playerInputs.Count > numberOfPlayers) UpdatePlayersNum(_inputManager.playerInputs.Count);
        print("players num: " );
        print(numberOfPlayers);
        // if (Input.GetKey(KeyCode.Escape))
        // {
        //     buttonPress.Play();
        //     ResetPlayers();
        // }
        // if ((Input.GetKey(KeyCode.Return)) && numberOfPlayers != 0)
        // {
        //     buttonPress.Play();
        //     enterRenderer.sprite = enterClicked;
        //     SceneManager.LoadScene("SampleScene");
        // }
        // countPlayers();
    }

    // private void countPlayers()
    // {
    //     numberOfPlayers = 0;
    //     foreach (var player in playersArr)
    //     {
    //         if (player)
    //         {
    //             numberOfPlayers++;
    //         }
    //     }
    // }

    private void ResetPlayers()
    {
        // for (int i = 0; i < playersArr.Count; i++)
        // {
        //     playersArr[i] = false;
        // }

        for (int i = 0; i < _inputManager.playerInputs.Count; i++)
        {
            Destroy(_inputManager.playerInputs[i].GameObject());
        }
        _inputManager.playerInputs.Clear();
        numberOfPlayers = 0;
        
        
        redKeysRenderer.sprite = greyRedKeys;
        greenKeysRenderer.sprite = greyGreenKeys;
        blueKeysRenderer.sprite = greyBlueKeys;
        yellowKeysRenderer.sprite = greyYellowKeys;
    }
    

    private void UpdatePlayersNum(int newNum)
    {
        for (int i = 0; i < newNum; i++)
        {
            // if (playersArr[i]) continue;
            // playersArr[i] = true;
            ActivatePlayer(i + 1);
            numberOfPlayers += 1;
        }
    }

    private void ActivatePlayer(int i)
    {
        if (i == 0) return;
        // playersArr[i - 1] = true;
        switch (i)
        {
            case 1:
                playerConnect1.Play();
                blueKeysRenderer.sprite = blueKeys;
                break;
            case 2:
                playerConnect2.Play();
                greenKeysRenderer.sprite = greenKeys;
                break;
            case 3:
                playerConnect3.Play();
                redKeysRenderer.sprite = redKeys;
                break;
            case 4:
                playerConnect4.Play();
                yellowKeysRenderer.sprite = yellowKeys;
                break;
            default:
                return;
        }
    }
    
    private void EnterFunc(InputAction.CallbackContext context)
    {
        if(numberOfPlayers == 0) ResetPlayers();
        if (numberOfPlayers != 0)
        {
            buttonPress.Play();
            enterRenderer.sprite = enterClicked;
            SceneManager.LoadScene("SampleScene");
        }
    }
    
    private void EscFunc(InputAction.CallbackContext context)
    {
        buttonPress.Play();
        ResetPlayers();
    }
}
