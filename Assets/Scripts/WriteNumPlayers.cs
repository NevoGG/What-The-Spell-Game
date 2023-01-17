using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WriteNumPlayers : MonoBehaviour
{
    public static int numberOfPlayers = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Return))
        {
            ActivateChoosePlayerScene();
        }
    }

    private void ActivateChoosePlayerScene()
    {
        SceneManager.LoadScene("ChoosePlayer");
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
