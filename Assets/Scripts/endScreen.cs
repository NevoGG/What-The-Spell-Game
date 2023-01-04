using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreen : MonoBehaviour
{
    
    [SerializeField] GameObject mainCam;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private GameObject buttonMainMenu;
    [SerializeField] private GameObject buttonPlayAgain;

    // Start is called before the first frame update
    void Start()
    {
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y,
            mainCam.transform.position.z);
        buttonMainMenu.SetActive(true);
        buttonPlayAgain.SetActive(true);
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
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("OpeningScene");
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    
}
