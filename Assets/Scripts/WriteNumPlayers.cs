using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WriteNumPlayers : MonoBehaviour
{
    public static int numberOfPlayers = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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