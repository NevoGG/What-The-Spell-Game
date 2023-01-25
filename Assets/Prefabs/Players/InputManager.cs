using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public List<PlayerInput> playerInputs = new List<PlayerInput>();
    
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInputs.Add(playerInput);
        // playerList[playerIdx].MoveScript = playerInput.GetComponent<MoveScript>();
        // playerIdx++;
        playerInput.transform.parent = transform;
    }
    
    //todo: add Player leave.
    //todo: add InputManager object in choose player scene
    //todo: find the Playerinput in main scene and connect with players.
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerInputs = new List<PlayerInput>();
    }
}
