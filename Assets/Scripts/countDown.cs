using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countDown : MonoBehaviour
{

    [SerializeField] private GameManager GM;
    [SerializeField] private AudioSource oneTwoThreeGo;
    public void SetCountDown()
    {
        
        // oneTwoThreeGo.Play();
        GM.CountDownFinish();
    }
    

}
