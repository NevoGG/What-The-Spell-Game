using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WriteNumPlayers : MonoBehaviour
{
    public static int numberOfPlayers = 1;
    // [SerializeField] private AudioSource buttonPress;
    // [SerializeField] private AudioSource music;
    private GameObject sFX;
     public static AudioSource menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        sFX = GameObject.FindGameObjectWithTag("SFX");
        sFX.GetComponent<SFXManager>().playMenuMusic();
        // menuMusic = music;
        // menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            sFX.GetComponent<SFXManager>().playButtonPress();
            // buttonPress.Play();
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Return))
        {
            sFX.GetComponent<SFXManager>().playButtonPress();
            // buttonPress.Play();
            // menuMusic.Stop();
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
}
