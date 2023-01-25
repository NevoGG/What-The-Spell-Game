using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WriteNumPlayers : MonoBehaviour
{
    private PlayerControls _controls;
    public static int numberOfPlayers = 1;
    [SerializeField] private AudioSource buttonPress;
    [SerializeField] private GameObject InputManager;
    private InputManager _inputManager;
    // Start is called before the first frame update
 
    void Awake()
    {
        GameObject inputManagerPrefab = GameObject.Find("InputManager(Clone)");
        if(inputManagerPrefab == null) _inputManager =  Instantiate(InputManager, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<InputManager>();
        else _inputManager = inputManagerPrefab.GetComponent<InputManager>();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            buttonPress.Play();
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Return))
        {
            
            // buttonPress.Play();
            ActivatetutorialScene();
        }
    }

    private void ActivatetutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Player1()
    {
        numberOfPlayers = 1;
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Player2()
    {
        numberOfPlayers = 2;
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Player3()
    {
        numberOfPlayers = 3;
        SceneManager.LoadScene("SampleScene");
    }
    
    public void Player4()
    {
        numberOfPlayers = 4;
        SceneManager.LoadScene("SampleScene");
    }
    
    private void EnterFunc(InputAction.CallbackContext context)
    {
        for (int i = 0; i < _inputManager.playerInputs.Count; i++)
        {
            Destroy(_inputManager.playerInputs[i].GameObject());
        }
        _inputManager.playerInputs.Clear();
        ActivatetutorialScene();
    }
    
    private void EscFunc(InputAction.CallbackContext context)
    {
        // buttonPress.Play();
        Application.Quit();
    }
}
