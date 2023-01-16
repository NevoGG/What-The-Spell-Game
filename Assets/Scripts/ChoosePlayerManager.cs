using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoosePlayerManager : MonoBehaviour
{

    public static List<bool> playersArr = new List<bool>{false, false, false, false};

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ResetPlayers();
        }
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Tutorial");
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

    public void ActivatePlayer1()
    {
        redKeysRenderer.sprite = redKeys;
        playersArr[0] = true;
    }
    
    public void ActivatePlayer2()
    {
        greenKeysRenderer.sprite = greenKeys;
        playersArr[1] = true;
    }
    
    public void ActivatePlayer3()
    {
        blueKeysRenderer.sprite = blueKeys;
        playersArr[2] = true;
    }
    
    public void ActivatePlayer4()
    {
        yellowKeysRenderer.sprite = yellowKeys;
        playersArr[3] = true;
    }
}
