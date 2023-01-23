using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePlayerManager : MonoBehaviour
{

    public static List<bool> playersArr = new List<bool>{false, false, false, false};
    public static int numberOfPlayers = 0;
    
    private PlayerInput1 controls1;
    protected InputAction move1;
    protected InputAction jump1;
    protected InputAction crouch1;
    protected InputAction dash1;
    
    private PlayerInput2 controls2;
    protected InputAction move2;
    protected InputAction jump2;
    protected InputAction crouch2;
    protected InputAction dash2;
    
    private PlayerInput3 controls3;
    protected InputAction move3;
    protected InputAction jump3;
    protected InputAction crouch3;
    protected InputAction dash3;
    
    private PlayerInput4 controls4;
    protected InputAction move4;
    protected InputAction jump4;
    protected InputAction crouch4;
    protected InputAction dash4;


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
    
    // Start is called before the first frame update
    void Awake()
    {
        controls1 = new PlayerInput1();
        controls2 = new PlayerInput2();
        controls3 = new PlayerInput3();
        controls4 = new PlayerInput4();
        ResetPlayers();
    }
    
    private void OnEnable()
    {
        move1 = controls1.Player.Move;
        move1.Enable();
        jump1 = controls1.Player.Jump;
        jump1.Enable();
        crouch1 = controls1.Player.Crouch;
        crouch1.Enable();
        dash1 =controls1.Player.Dash;
        dash1.Enable();
        move1.performed += ActivatePlayer1;
        dash1.performed += ActivatePlayer1;
        jump1.performed += ActivatePlayer1;
        crouch1.performed += ActivatePlayer1;
        
        move2 = controls2.Player.Move;
        move2.Enable();
        jump2 = controls2.Player.Jump;
        jump2.Enable();
        crouch2 = controls2.Player.Crouch;
        crouch2.Enable();
        dash2 =controls2.Player.Dash;
        dash2.Enable();
        move2.performed += ActivatePlayer2;
        dash2.performed += ActivatePlayer2;
        jump2.performed += ActivatePlayer2;
        crouch2.performed += ActivatePlayer2;
        
        move3 = controls3.Player.Move;
        move3.Enable();
        jump3 = controls3.Player.Jump;
        jump3.Enable();
        crouch3 = controls3.Player.Crouch;
        crouch3.Enable();
        dash3 =controls3.Player.Dash;
        dash3.Enable();
        move3.performed += ActivatePlayer3;
        dash3.performed += ActivatePlayer3;
        jump3.performed += ActivatePlayer3;
        crouch3.performed += ActivatePlayer3;
        
        move4 = controls4.Player.Move;
        move4.Enable();
        jump4 = controls4.Player.Jump;
        jump4.Enable();
        crouch4 = controls4.Player.Crouch;
        crouch4.Enable();
        dash4 =controls4.Player.Dash;
        dash4.Enable();
        move4.performed += ActivatePlayer4;
        dash4.performed += ActivatePlayer4;
        jump4.performed += ActivatePlayer4;
        crouch4.performed += ActivatePlayer4;
    }
    
    private void OnDisable()
    {
        move1.Disable();
        jump1.Disable();
        dash1.Disable();
        crouch1.Disable();
        
        move2.Disable();
        jump2.Disable();
        dash2.Disable();
        crouch2.Disable();
        
        move3.Disable();
        jump3.Disable();
        dash3.Disable();
        crouch3.Disable();
        
        move4.Disable();
        jump4.Disable();
        dash4.Disable();
        crouch4.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        // if (move1.ReadValue<float>() != 0) ActivatePlayer1();
        // if(move1.ReadValue<float>() != 0)
        // if(move1.ReadValue<float>() != 0) 
        //     if(move1.ReadValue<float>() != 0)
        
        if (Input.GetKey(KeyCode.Escape))
        {
            buttonPress.Play();
            ResetPlayers();
        }
        if (Input.GetKey(KeyCode.Return) && numberOfPlayers != 0)
        {
            buttonPress.Play();
            enterRenderer.sprite = enterClicked;
            SceneManager.LoadScene("SampleScene");
        }
        countPlayers();
    }

    private void countPlayers()
    {
        numberOfPlayers = 0;
        foreach (var player in playersArr)
        {
            if (player)
            {
                numberOfPlayers++;
            }
        }
    }

    private void ResetPlayers()
    {
        for (int i = 0; i < playersArr.Count; i++)
        {
            playersArr[i] = false;
        }
        redKeysRenderer.sprite = greyRedKeys;
        greenKeysRenderer.sprite = greyGreenKeys;
        blueKeysRenderer.sprite = greyBlueKeys;
        yellowKeysRenderer.sprite = greyYellowKeys;
    }

    public void ActivatePlayer1(InputAction.CallbackContext context)
    {
        if (playersArr[0]) return;
        playerConnect1.Play();
        blueKeysRenderer.sprite = blueKeys;
        playersArr[0] = true;

    }
    
    public void ActivatePlayer2(InputAction.CallbackContext context)
    {
        if (playersArr[1]) return;
        playerConnect2.Play();
        greenKeysRenderer.sprite = greenKeys;
        playersArr[1] = true;
    }
    
    public void ActivatePlayer3(InputAction.CallbackContext context)
    {
        if (playersArr[2]) return;
        playerConnect3.Play();
        redKeysRenderer.sprite = redKeys;
        playersArr[2] = true;
    }
    
    public void ActivatePlayer4(InputAction.CallbackContext context)
    {
        if (playersArr[3]) return;
        playerConnect4.Play();
        yellowKeysRenderer.sprite = yellowKeys;
        playersArr[3] = true;
    }
}
