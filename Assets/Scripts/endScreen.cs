using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{
    
    [SerializeField] GameObject mainCam;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private GameObject buttonMainMenu;
    [SerializeField] private GameObject buttonPlayAgain;
    [SerializeField] private List<float> sizeIntoBubble;
    [SerializeField] private Vector3 textSpeed = new  Vector3(0.01f, 0, 0);
    private GameObject textWinner;
    private Vector3 initTextPos;
    [SerializeField] private float leftBounderText;

    [SerializeField] private SpriteRenderer spriteRendererBubble;
    [SerializeField] private SpriteRenderer spriteRendererText;
    [SerializeField] private Sprite BlueBubble;
    [SerializeField] private Sprite YellowBubble;
    [SerializeField] private Sprite RedBubble;
    [SerializeField] private Sprite GreenBubble;
    [SerializeField] private Sprite blueWin;
    [SerializeField] private Sprite yellowWin;
    [SerializeField] private Sprite redWin;
    [SerializeField] private Sprite greenWin;
    
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource win2;
    [SerializeField] private AudioSource buttonPress;
    private InputManager _inputManager;
    private PlayerControls _controls;

    public bool isActive = false;
    // Start is called before the first frame update

    void Start()
    {
        _inputManager = GameObject.Find("InputManager(Clone)").GetComponent<InputManager>();
        // fede: decide on which sound
        // win.Play();
        win2.Play();
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y,
            mainCam.transform.position.z);
        buttonMainMenu.SetActive(true);
        buttonPlayAgain.SetActive(true);
        textWinner = transform.GetChild(1).gameObject;
        initTextPos = textWinner.transform.position;
        
        _controls = new PlayerControls();
        _controls.UI.Enter.Enable();
        _controls.UI.Enter.performed += EnterFunc;
        _controls.UI.Escape.Enable();
        _controls.UI.Escape.performed += EscFunc;
    }

    public void EndGame(List<Player> rankList)
    {
        foreach (var player in rankList)
        {
            if (!player.gameObject.activeSelf)
            {
                player.gameObject.SetActive(true);
                player.TurnToBlob();
            }
        }

        for (int i = 0; i < rankList.Count; i++)
        {
            rankList[i].MoveToPosition(positions[i]);
            rankList[i].OperateEndGame(sizeIntoBubble[i]); //todo:activate
        }

        SetSprites(rankList[0]);

    }

    private void SetSprites(Player player)
    {
        switch (player.gameObject.name)
        {
            case "PlayerBlue":
                spriteRendererBubble.sprite = BlueBubble;
                spriteRendererText.sprite = blueWin;
                break;
            
            case "PlayerGreen":
                spriteRendererBubble.sprite = GreenBubble;
                spriteRendererText.sprite = greenWin;
                break;

            case "PlayerYellow":
                spriteRendererBubble.sprite = YellowBubble;
                spriteRendererText.sprite = yellowWin;
                break;

            case "PlayerRed":
                spriteRendererBubble.sprite = RedBubble;
                spriteRendererText.sprite = redWin;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            buttonPress.Play();
            Debug.Log("presed");
            PlayAgain();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            buttonPress.Play();
            MainMenu();
        }
        textWinner.transform.position -= textSpeed * Time.deltaTime;
        if (textWinner.transform.position.x < leftBounderText)
        {
            textWinner.transform.position = initTextPos;
        }
    }

    public void MainMenu()
    {
        isActive = false;
        SceneManager.LoadScene("OpeningScene");
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    private void EnterFunc(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            // buttonPress.Play();
            PlayAgain();
        }
    }
    
    private void EscFunc(InputAction.CallbackContext context)
    {
        if (isActive)
        {
            for (int i = 0; i < _inputManager.playerInputs.Count; i++)
            {
                Destroy(_inputManager.playerInputs[i].GameObject());
            }
            _inputManager.playerInputs.Clear();
            print("ended through endscreen");
            MainMenu();
        }
    }
    
}
