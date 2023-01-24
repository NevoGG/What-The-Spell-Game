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
    
    // [SerializeField] private AudioSource win;
    // [SerializeField] private AudioSource win2;
    // [SerializeField] private AudioSource buttonPress;
    private GameObject sFX;
    // Start is called before the first frame update
    void Start()
    {
        // fede: decide on which sound
        // win.Play();
        // win2.Play();
        sFX = GameObject.FindGameObjectWithTag("SFX");
        // sFX.GetComponent<SFXManager>().stopMenuMusic();
        sFX.GetComponent<SFXManager>().playMenuMusic();
        sFX.GetComponent<SFXManager>().playWin();
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y,
            mainCam.transform.position.z);
        buttonMainMenu.SetActive(true);
        buttonPlayAgain.SetActive(true);
        textWinner = transform.GetChild(1).gameObject;
        initTextPos = textWinner.transform.position;
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
            // buttonPress.Play();
            sFX.GetComponent<SFXManager>().playButtonPress();
            sFX.GetComponent<SFXManager>().pauseMenuMusic();
            Debug.Log("presed");
            PlayAgain();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            // buttonPress.Play();
            sFX.GetComponent<SFXManager>().playButtonPress();
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
        SceneManager.LoadScene("OpeningScene");
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    
}
