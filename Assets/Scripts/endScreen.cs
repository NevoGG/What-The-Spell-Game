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
                print("work");
                player.TurnToBlob();
            }
        }

        for (int i = 0; i < rankList.Count; i++)
        {
            rankList[i].MoveToPosition(positions[i]);
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
