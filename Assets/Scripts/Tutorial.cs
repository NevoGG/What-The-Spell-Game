using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private AudioSource buttonPress;
    [SerializeField] private float waitTime = 19f;
    // [SerializeField] private AudioSource menuMusic;
    private GameObject sFX;
    private bool active;
    private PlayerControls _controls;
    void Start()
    {
        sFX = GameObject.FindGameObjectWithTag("SFX");
        // menuMusic.Play();
        active = true;
        _controls = new PlayerControls();
        _controls.UI.Enter.Enable();
        _controls.UI.Enter.performed += EnterFunc;
        _controls.UI.Escape.Enable();
        _controls.UI.Escape.performed += EscFunc;
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.Return))
        {
            
            sFX.GetComponent<SFXManager>().playButtonPress();
            // buttonPress.Play();
            // menuMusic.Stop();
            ActivateChoosePlayerScene();
        }
        if (active)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                active = false;
                ActivateChoosePlayerScene();
            }
        }
    }
    private void ActivateChoosePlayerScene()
    {
        SceneManager.LoadScene("ChoosePlayer");
    }
    
    private void EnterFunc(InputAction.CallbackContext context)
    {
        // buttonPress.Play();
        ActivateChoosePlayerScene();
    }
    
    private void EscFunc(InputAction.CallbackContext context)
    {
    }
}
