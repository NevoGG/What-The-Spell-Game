using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;



public class UiControler : MonoBehaviour
{
    [SerializeField] private GameObject player1Img;
    [SerializeField] private GameObject player2Img;
    [SerializeField] private GameObject player3Img;
    [SerializeField] private GameObject player4Img;
    [SerializeField] private GameObject player1Exp;
    [SerializeField] private GameObject player2Exp;
    [SerializeField] private GameObject player3Exp;
    [SerializeField] private GameObject player4Exp;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player3;
    [SerializeField] private GameObject Player4;
    [SerializeField] private GameObject gameManager;

    private int playersNum;
    
    private List<Tuple<GameObject, GameObject,GameObject>> imagesLst;

    private const string ANIMAL1 = "bunny-";
    private const string ANIMAL2 = "fox-";
    private const string ANIMAL3 = "bigfoot-";
    private const string ANIMAL4 = "hippogriff-";
    private const string ANIMAL5 = "unicorn-";
    private const string ANIMAL6 = "dragon-";
    
    private const string PLAYER1COLOR = "blue";
    private const string PLAYER2COLOR = "red";
    private const string PLAYER3COLOR = "yellow";
    private const string PLAYER4COLOR = "green";
    private const string DEAD_PLAYER_COLOR = "grey";
    
    private const string DOTPNG = ".png";
    private const string BASE_ANIMAL_URL = "UIArt/Portraits/portrait_";
    private const string BASE_EXP_URL = "UIArt/Variations/exp_";
    
    // private const string EXP_1_0 = "UIArt/Variations/exp_1-0.png";
    // private const string EXP_1_1 = "UIArt/Variations/exp_1-1.png";
    // private const string EXP_2_0 = "UIArt/Variations/exp_2-0.png";
    // private const string EXP_2_1 = "UIArt/Variations/exp_2-1.png";
    // private const string EXP_2_2 = "UIArt/Variations/exp_2-2.png";
    // private const string EXP_3_0 = "UIArt/Variations/exp_3-0.png";
    // private const string EXP_3_1 = "UIArt/Variations/exp_3-1.png";
    // private const string EXP_3_2 = "UIArt/Variations/exp_3-2.png";
    // private const string EXP_3_3 = "UIArt/Variations/exp_3-3.png";
    // private const string EXP_4_0 = "UIArt/Variations/exp_4-0.png";
    // private const string EXP_4_1 = "UIArt/Variations/exp_4-1.png";
    // private const string EXP_4_2 = "UIArt/Variations/exp_4-2.png";
    // private const string EXP_4_3 = "UIArt/Variations/exp_4-3.png";
    // private const string EXP_4_4 = "UIArt/Variations/exp_4-4.png";
    // private const string EXP_5_0 = "UIArt/Variations/exp_5-0.png";
    // private const string EXP_5_1 = "UIArt/Variations/exp_5-1.png";
    // private const string EXP_5_2 = "UIArt/Variations/exp_5-2.png";
    // private const string EXP_5_3 = "UIArt/Variations/exp_5-3.png";
    // private const string EXP_5_4 = "UIArt/Variations/exp_5-4.png";
    // private const string EXP_5_5 = "UIArt/Variations/exp_5-5.png";
    
    
    
    // public const string BUNNY_GRAY = "UIArt/Portraits/portrait_bunny-gray.png";
    // public const string BUNNY_BLUE = "UIArt/Portraits/portrait_bunny-blue.png";
    // public const string BUNNY_GREEN = "UIArt/Portraits/portrait_bunny-green.png";
    // public const string BUNNY_YELLOW = "UIArt/Portraits/portrait_bunny-yellow.png";
    // public const string BUNNY_RED = "UIArt/Portraits/portrait_bunny-red.png";
    //
    // public const string DRAGON_GRAY = "UIArt/Portraits/portrait_dragon-gray.png";
    // public const string DRAGON_BLUE = "UIArt/Portraits/portrait_dragon-blue.png";
    // public const string DRAGON_GREEN = "UIArt/Portraits/portrait_dragon-green.png";
    // public const string DRAGON_YELLOW = "UIArt/Portraits/portrait_dragon-yellow.png";
    // public const string DRAGON_RED = "UIArt/Portraits/portrait_dragon-red.png";
    //
    // public const string FOX_GRAY = "UIArt/Portraits/portrait_fox-gray.png";
    // public const string FOX_BLUE = "UIArt/Portraits/portrait_fox-blue.png";
    // public const string FOX_GREEN = "UIArt/Portraits/portrait_fox-green.png";
    // public const string FOX_YELLOW = "UIArt/Portraits/portrait_fox-yellow.png";
    // public const string FOX_RED = "UIArt/Portraits/portrait_fox-red.png";
    //
    // public const string UNICORN_GRAY = "UIArt/Portraits/portrait_unicorn-gray.png";
    // public const string UNICORN_BLUE = "UIArt/Portraits/portrait_unicorn-blue.png";
    // public const string UNICORN_GREEN = "UIArt/Portraits/portrait_unicorn-green.png";
    // public const string UNICORN_YELLOW = "UIArt/Portraits/portrait_unicorn-yellow.png";
    // public const string UNICORN_RED = "UIArt/Portraits/portrait_unicorn-red.png";
    //
    // public const string YETI_GRAY = "Assets/UIArt/Portraits/portrait_yeti-gray.png";
    // public const string YETI_BLUE = "Assets/UIArt/Portraits/portrait_yeti-blue.png";
    // public const string YETI_GREEN = "Assets/UIArt/Portraits/portrait_yeti-green.png";
    // public const string YETI_YELLOW = "Assets/UIArt/Portraits/portrait_yeti-yellow.png";
    // public const string YETI_RED = "Assets/UIArt/Portraits/portrait_yeti-red.png";
    //
    // public const string HIPPOGRIFF_GRAY = "Assets/UIArt/Portraits/portrait_hippogriff-gray.png";
    // public const string HIPPOGRIFF_BLUE = "Assets/UIArt/Portraits/portrait_hippogriff-blue.png";
    // public const string HIPPOGRIFF_GREEN = "Assets/UIArt/Portraits/portrait_hippogriff-green.png";
    // public const string HIPPOGRIFF_YELLOW = "Assets/UIArt/Portraits/portrait_hippogriff-yellow.png";
    // public const string HIPPOGRIFF_RED = "Assets/UIArt/Portraits/portrait_hippogriff-red.png";


    private string getExpImg(int totalExp, int currentExp)
    {
        string toRet = BASE_EXP_URL;
        switch (totalExp)
        {
            case (1):
                toRet += "1-";
                break;
            case (2):
                toRet += "2-";
                break;
            case (3):
                toRet += "3-";
                break;
            case (4):
                toRet += "4-";
                break;
            case (5):
                toRet += "5-";
                break;
        }
        switch (currentExp)
        {
            case (0):
                return toRet + "0" + DOTPNG;
            case (1):
                return toRet + "1" + DOTPNG;
            case (2):
                return toRet + "2" + DOTPNG;
            case (3):
                return toRet + "3" + DOTPNG;
            case (4):
                return toRet + "4" + DOTPNG;
            case (5):
                return toRet + "5" + DOTPNG;
        }
        return toRet;
    }
private void InitImgLst()
    {
        Tuple<GameObject, GameObject, GameObject> tup1 = new Tuple<GameObject, GameObject, GameObject>(player1Img, player1Exp, Player1);
        Tuple<GameObject, GameObject, GameObject> tup2 = new Tuple<GameObject, GameObject, GameObject>(player2Img, player2Exp, Player2);
        Tuple<GameObject, GameObject, GameObject> tup3 = new Tuple<GameObject, GameObject, GameObject>(player3Img, player3Exp, Player3);
        Tuple<GameObject, GameObject, GameObject> tup4 = new Tuple<GameObject, GameObject, GameObject>(player4Img, player4Exp, Player4);
        imagesLst = new List<Tuple<GameObject, GameObject, GameObject>>();
        imagesLst.Add(tup1);
        imagesLst.Add(tup2);
        imagesLst.Add(tup3);
        imagesLst.Add(tup4);
    }
    
    
    private string AddAnimal( int animalIndx)
    {
        switch (animalIndx)
        {
            case 1:
                return BASE_ANIMAL_URL + ANIMAL1;
            case 2:
                return BASE_ANIMAL_URL + ANIMAL2;
            case 3:
                return BASE_ANIMAL_URL + ANIMAL3;
            case 4:
                return BASE_ANIMAL_URL + ANIMAL4;
            case 5:
                return BASE_ANIMAL_URL + ANIMAL5;
            case 6:
                return BASE_ANIMAL_URL + ANIMAL6;
        }
        return BASE_ANIMAL_URL + ANIMAL1;
    }
    private string AddColor(string baseUrl, int colorIndx)
    {
        switch (colorIndx)
        {
            case 1:
                return baseUrl + PLAYER1COLOR + DOTPNG;
            case 2:
                return baseUrl + PLAYER2COLOR + DOTPNG;
            case 3:
                return baseUrl + PLAYER3COLOR + DOTPNG;
            case 4:
                return baseUrl + PLAYER4COLOR + DOTPNG;
        }
        return baseUrl + DEAD_PLAYER_COLOR + DOTPNG;
    }
    
    
    private void setPlayer(int playerIndex, bool state)
    {
        imagesLst[playerIndex].Item1.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(AddColor(AddAnimal(1),playerIndex + 1));
        imagesLst[playerIndex].Item2.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(getExpImg(5,0));
        imagesLst[playerIndex].Item1.SetActive(state);
        imagesLst[playerIndex].Item2.SetActive(state);
    }

    private void updatePlayer(int playerIndex, int animal, int totalExp, int currentExp)
    {
        imagesLst[playerIndex].Item1.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(AddColor(AddAnimal(animal),playerIndex + 1));
        imagesLst[playerIndex].Item2.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(getExpImg(totalExp,currentExp));
    }
    private void SetActivePlayers()
    {
        switch (playersNum)
        {
            case 1:
                setPlayer(0, true);
                setPlayer(1, false);
                setPlayer(2, false);
                setPlayer(3, false);
                break;
            case 2:
                setPlayer(0, true);
                setPlayer(1, true);
                setPlayer(2, false);
                setPlayer(3, false);
                break;
            case 3:
                setPlayer(0, true);
                setPlayer(1, true);
                setPlayer(2, true);
                setPlayer(3, false);
                break;
            case 4:
                setPlayer(0, true);
                setPlayer(1, true);
                setPlayer(2, true);
                setPlayer(3, true);
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playersNum = gameManager.GetComponent<GameManager>().numberOfPlayers;
        InitImgLst();
        SetActivePlayers();
    }

    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0 ; i < playersNum ; i++ )
        {
            if (imagesLst[i].Item3.activeSelf && imagesLst[i].Item3.GetComponent<Player>().hasChanged)
            {
                var tup = imagesLst[i].Item3.GetComponent<Player>().GetScore();
                updatePlayer(i,tup.Item1, tup.Item2, tup.Item3);
                imagesLst[i].Item3.GetComponent<Player>().hasChanged = false;
            }

            if (!imagesLst[i].Item3.activeSelf)
            {
                imagesLst[i].Item1.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(AddColor(AddAnimal(1),0));
                imagesLst[i].Item2.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(getExpImg(0,0));
            }
        }
    }
}
